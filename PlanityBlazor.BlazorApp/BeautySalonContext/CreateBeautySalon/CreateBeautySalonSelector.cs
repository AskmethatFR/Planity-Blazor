using PlanityBlazor.BlazorApp.Components;
using PlanityBlazor.BlazorApp.Shared.Reactive;

namespace PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;

public record CreateBeautySalonSelector : IReactiveSelector<BeautySalonState, CreateBeautySalonViewModel>
{
    readonly CreateBeautySalonViewModel _viewModel = new CreateBeautySalonViewModel();

    public CreateBeautySalonViewModel OnNext(BeautySalonState value)
    {
        if (value.Progress)
        {
            _viewModel.State = ViewModelState.Progress;
        }

        if (!string.IsNullOrEmpty(value.Error))
        {
            _viewModel.State = ViewModelState.Error;
            _viewModel.Message = value.Error;
        }

        if (value.Success)
        {
            _viewModel.State = ViewModelState.Completed;
        }

        return _viewModel;
    }

    public CreateBeautySalonViewModel OnError(Exception exception)
    {
        return new CreateBeautySalonViewModel() { State = ViewModelState.Error, Message = exception.Message };
    }

    public CreateBeautySalonViewModel OnCompleted()
    {
        return _viewModel;
    }
}