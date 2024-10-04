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
    public async Task HandleCreateABeautySalonAction(CreateABeautySalonAction action, IDispatcher dispatcher)
    {
        try
        {
            action.ValidateAndThrow();
        }
        catch (Exception e)
        {
            dispatcher.Dispatch(new ErrorOnCreatingBeautySalon(e.Message));
            return;
        }


        var beautySalon = new BeautySalon(action.BeautySalon);
        var result = await _beautySalonGateway.PostBeautySalon(beautySalon);
        if (result)
        {
            dispatcher.Dispatch(new CreateABeautySalonCompleteAction(beautySalon));
        }
    }

    [ReducerMethod]
    public static BeautySalonState ReduceCreateABeautySalonCompleteAction(BeautySalonState state,
        CreateABeautySalonCompleteAction action) =>
        state with
        {
            Salons = state.Salons.Append(action.BeautySalon).ToList(), Error = String.Empty, Success = true,
            Progress = false
        };

    [ReducerMethod]
    public static BeautySalonState ReduceErrorOnCreatingBeautySalon(BeautySalonState state,
        ErrorOnCreatingBeautySalon action) =>
        state with { Error = action.Error, Success = false };
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

    [ReducerMethod]
    public static BeautySalonState Reduce(BeautySalonState state,
        CreateABeautySalonAction action) =>
        state with { Error = String.Empty, Success = false, Progress = true };
}

public record CreateABeautySalonCompleteAction(BeautySalon BeautySalon);