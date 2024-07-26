using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;

namespace PlanityBlazor.BlazorApp.Shared.Reactive;

public class AppSelector<TState, TReturnType> : IObservable<TReturnType>
    where TState : class
    where TReturnType : class
{
    private readonly IState<TState> _state;
    private readonly IReactiveSelector<TState, TReturnType> _subject;

    public AppSelector(IState<TState> state, IReactiveSelector<TState, TReturnType> subject)
    {
        _state = state;
        _subject = subject;
    }
    
    public IDisposable Subscribe(IObserver<TReturnType> observer)
    {
        _state.StateChanged += (sender, _) =>
        {
            observer.OnNext(_subject.OnNext(((IState<TState>)sender).Value));
        };
        
        return System.Reactive.Disposables.Disposable.Empty; 
    }
}