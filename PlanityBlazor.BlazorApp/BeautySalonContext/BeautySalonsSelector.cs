using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Fluxor;
using PlanityBlazor.BlazorApp.Shared.Reactive;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;

public record BeautySalonsSelector : IReactiveSelector<BeautySalonState, BeautySalonsViewModel>
{
    private readonly BeautySalonsViewModel _currentBeautySalons = new BeautySalonsViewModel();

    public BeautySalonsViewModel OnNext(BeautySalonState value)
    {
        if (value.Progress)
            return this._currentBeautySalons with { Status = ViewModelState.Progress, LoadingMessage = "Loading..." };

        if (value.Salons.Any())
            return this._currentBeautySalons with
            {
                Status = ViewModelState.Completed, BeautySalons = value.Salons.Select(x => x.Name).ToList()
            };

        return _currentBeautySalons with { Status = ViewModelState.Nothing, NothingMessage = "Nothing here" };
    }

    public BeautySalonsViewModel OnError(Exception exception)
    {
        return _currentBeautySalons with { Status = ViewModelState.Error, ErrorMessage = "An error occured" };
    }

    public BeautySalonsViewModel OnCompleted()
    {
        throw new NotImplementedException();
    }
}