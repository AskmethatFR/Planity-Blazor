using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;

public record GetSalonsActionCompleteEffect(List<string> Salons, List<BeautySalon> Salons2);

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
        var beautySalons = await _gateway.GetBeautySalonsAsync();
        dispatcher.Dispatch(new GetSalonsActionCompleteEffect(beautySalons.Select(x => x.Name).ToList(), beautySalons));
    }

    [ReducerMethod]
    public static BeautySalonState ReduceGetSalonsActionCompleteEffect(BeautySalonState state,
        GetSalonsActionCompleteEffect action) =>
        state with { Progress = false, Salons = action.Salons2 };

    [ReducerMethod]
    public static BeautySalonState ReduceGetSalonsAction(BeautySalonState state, GetSalonsAction action) =>
        state with { Progress = true };
}