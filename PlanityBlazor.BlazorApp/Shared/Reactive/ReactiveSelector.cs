namespace PlanityBlazor.BlazorApp.Shared.Reactive;

public abstract record ReactiveSelector<TState, TViewModel>: IDisposable
{
    protected readonly TState _state;

    protected ReactiveSelector(TState state)
    {
        _state = state;
    }
    
    public abstract void Dispose();
    protected abstract void Next();
    public abstract TViewModel Get();
}