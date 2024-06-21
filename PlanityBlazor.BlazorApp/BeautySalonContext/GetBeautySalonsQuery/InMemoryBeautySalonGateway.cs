namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public class InMemoryBeautySalonGateway : IBeautySalonGateway
{
    public List<string> All { get; set; } = new List<string>();

    public Task<List<string>> GetBeautySalonsAsync()
    {
        return Task.FromResult(All);
    }
}