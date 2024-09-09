using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;

public class CreateABeautySalon
{
    private readonly IState<MyState> _beautySalonState;
    private readonly InMemoryBeautySalonGateway _beautySalonGateway;

    public CreateABeautySalon(IState<MyState> beautySalonState, InMemoryBeautySalonGateway beautySalonGateway)
    {
        _beautySalonState = beautySalonState;
        _beautySalonGateway = beautySalonGateway;
    }

    public void Execute(string beautySalon)
    {
        var result = _beautySalonGateway.PostBeautySalon(beautySalon);

        if (result)
        {
            _beautySalonState.Value.Salons.Add(beautySalon);
        }
    }
}