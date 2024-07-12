namespace PlanityBlazor.BlazorApp.Shared.Reactive;

public delegate TReturnType Selector<in TState, out TReturnType>(TState state);

public class AppSelector<TState, TReturnType, TSelector> where TSelector : ReactiveSelector<TState, TReturnType>
{
    private readonly TState _state;
    private readonly TReturnType _returnType;
    private readonly Selector<TState, TReturnType> _selector;

    public AppSelector(TState state)
    {
        _state = state;
        _selector = currentState =>
            ((TSelector)Activator.CreateInstance(typeof(TSelector), new object[] { state })).Get();
    }

    public TReturnType Select() => _selector(_state);
}