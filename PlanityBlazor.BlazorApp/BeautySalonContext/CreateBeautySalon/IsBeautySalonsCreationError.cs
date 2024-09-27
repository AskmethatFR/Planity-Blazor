using PlanityBlazor.BlazorApp.Shared.Reactive;

namespace PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;

public class CreationError(bool isError)
{
    public bool IsError { get; set; } = isError;
}

public record IsBeautySalonsCreationError : IReactiveSelector<BeautySalonState, CreationError>
{
    readonly CreationError _creationError = new CreationError(false);

    public CreationError OnNext(BeautySalonState value)
    {
        _creationError.IsError = !string.IsNullOrEmpty(value.Error);
        return _creationError;
    }

    public CreationError OnError(Exception exception)
    {
        return new CreationError(true);
    }

    public CreationError OnCompleted()
    {
        return _creationError;
    }
}