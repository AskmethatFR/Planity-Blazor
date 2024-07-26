namespace PlanityBlazor.BlazorApp.BeautySalonContext;

public record BeautySalonsViewModel
{
    public List<string> BeautySalons { get; init; } = new List<string>();
    public ViewModelState Status { get; set; }
    public string NothingMessage { get; set; }
    public string LoadingMessage { get; set; }
    public string ErrorMessage { get; set; }
}
