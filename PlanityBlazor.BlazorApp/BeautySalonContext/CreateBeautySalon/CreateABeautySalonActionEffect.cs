using FluentValidation;
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
        try
        {
            action.ValidateAndThrow();
        }
        catch (Exception e)
        {
            dispatcher.Dispatch(new ErrorOnCreatingBeautySalon(e.Message));
            return Task.CompletedTask;
        }


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
        state with { Salons = state.Salons.Append(action.BeautySalon).ToList(), Error = String.Empty };

    [ReducerMethod]
    public static BeautySalonState ReduceErrorOnCreatingBeautySalon(BeautySalonState state,
        ErrorOnCreatingBeautySalon action) =>
        state with { Error = action.Error };
}

public record ErrorOnCreatingBeautySalon(string Error);

public class CreateABeautySalonAction : AbstractValidator<CreateABeautySalonAction>
{
    public string BeautySalon { get; }

    public CreateABeautySalonAction(string beautySalon)
    {
        BeautySalon = beautySalon;

        RuleFor(x => x.BeautySalon).NotEmpty();
    }

    public void ValidateAndThrow()
    {
        var result = Validate(this);
        if (!result.IsValid)
        {
            throw new Exception(result.ToString());
        }
    }
}

public record CreateABeautySalonCompleteAction(BeautySalon BeautySalon);