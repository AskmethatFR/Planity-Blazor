namespace PlanityBlazor.BlazorApp.Components;

public record CreateBeautySalonViewModel
{
    public ViewModelState State { get; set; }
    public string Message { get; set; } = string.Empty;
}