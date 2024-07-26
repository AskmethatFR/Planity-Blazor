using System.Reactive.Disposables;
using System.Reactive.Subjects;
using Fluxor;
using PlanityBlazor.BlazorApp.Shared.Reactive;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;

public record BeautySalonsSelector : IReactiveSelector<MyState, BeautySalonsViewModel>
{
    private readonly BeautySalonsViewModel _currentBeautySalons = new BeautySalonsViewModel();

    public BeautySalonsViewModel OnNext(MyState value)
    {
       return _currentBeautySalons with { BeautySalons = value.Salons, InProgress = value.Progress, Error = false};
    }

    public BeautySalonsViewModel OnError(Exception exception)
    {
       return _currentBeautySalons with { Error = true };
    }

    public BeautySalonsViewModel OnCompleted()
    {
        throw new NotImplementedException();
    }
}
