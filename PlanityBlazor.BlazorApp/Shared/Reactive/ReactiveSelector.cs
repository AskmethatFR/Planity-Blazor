using PlanityBlazor.BlazorApp.BeautySalonContext;

namespace PlanityBlazor.BlazorApp.Shared.Reactive;

public interface IReactiveSelector<in TState, out TViewModel> : System.Reactive.IObserver<TState, TViewModel>;