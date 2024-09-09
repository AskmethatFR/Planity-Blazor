using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;


public record GetSalonsActionCompleteEffect(List<string> Salons);
public record GetSalonsAction();

public class GetSalonsActionEffect
{
    private readonly IBeautySalonGateway _gateway;

    public GetSalonsActionEffect(IBeautySalonGateway gateway)
    {
        this._gateway = gateway;
    }

    [EffectMethod]
    public async Task Handle(GetSalonsAction action, IDispatcher dispatcher)
    {
        dispatcher.Dispatch(new GetSalonsActionCompleteEffect(await _gateway.GetBeautySalonsAsync()));
    }

    [ReducerMethod]
    public static BeautySalonState ReduceGetSalonsActionCompleteEffect(BeautySalonState state, GetSalonsActionCompleteEffect action) =>
        state with { Salons = action.Salons, Progress = false };

    [ReducerMethod]
    public static BeautySalonState ReduceGetSalonsAction(BeautySalonState state, GetSalonsAction action) =>
        state with { Progress = true };
}
