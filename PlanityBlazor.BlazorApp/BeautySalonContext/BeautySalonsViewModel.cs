namespace PlanityBlazor.BlazorApp.BeautySalonContext;

public record BeautySalonsViewModel
{
    public List<string> BeautySalons { get; init; } = new List<string>();
    public bool InProgress { get; set; }
    public bool Error { get; set; }
}