using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;

public class CreateABeautySalonActionEffect
{
    private readonly IBeautySalonGateway _beautySalonGateway;

    public CreateABeautySalonActionEffect(IBeautySalonGateway beautySalonGateway)
    {
        _beautySalonGateway = beautySalonGateway;
    }

    [EffectMethod]
    public Task HandleCreateABeautySalonAction(CreateABeautySalonAction action, IDispatcher dispatcher)
    {
        var beautySalon = new BeautySalon(action.BeautySalon);
        var result = _beautySalonGateway.PostBeautySalon(beautySalon);
        if (result)
        {
            dispatcher.Dispatch(new CreateABeautySalonCompleteAction(beautySalon));
        }

        return Task.CompletedTask;
    }

    [ReducerMethod]
    public static BeautySalonState ReduceCreateABeautySalonAction(BeautySalonState state,
        CreateABeautySalonCompleteAction action) =>
        state with { Salons = state.Salons.Append(action.BeautySalon).ToList() };
}

public record CreateABeautySalonAction(string BeautySalon);

public record CreateABeautySalonCompleteAction(BeautySalon BeautySalon);