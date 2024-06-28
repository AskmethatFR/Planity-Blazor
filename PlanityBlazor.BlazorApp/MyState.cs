using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp;

[FeatureState]
public record MyState
{
    public List<string> Salons { get; set; } = new List<string>();
    public bool Progress { get; set; } = false;
}

public record GetSalonsAction();

public record GetSalonsActionCompleteEffect(List<string> Salons);

public class GetSalonsActionEffect
{
    private readonly IBeautySalonGateway gateway;

    public GetSalonsActionEffect(IBeautySalonGateway gateway)
    {
        this.gateway = gateway;
    }

    [EffectMethod]
    public async Task Handle(GetSalonsAction action, IDispatcher dispatcher)
    {
        // await Task.Delay(1500);
        dispatcher.Dispatch(new GetSalonsActionCompleteEffect(await this.gateway.GetBeautySalonsAsync()));
    }

    [ReducerMethod]
    public static MyState ReduceGetSalonsActionCompleteEffect(MyState state, GetSalonsActionCompleteEffect action) =>
        state with { Salons = action.Salons, Progress = false };

    [ReducerMethod]
    public static MyState ReduceGetSalonsAction(MyState state, GetSalonsAction action) =>
        state with { Progress = true };
}
