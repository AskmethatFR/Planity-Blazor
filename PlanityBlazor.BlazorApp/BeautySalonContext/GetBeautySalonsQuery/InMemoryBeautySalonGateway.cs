namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public class InMemoryBeautySalonGateway : IBeautySalonGateway
{
    private readonly TimeSpan _delay;

    public InMemoryBeautySalonGateway(int delay = 0)
    {
        _delay = TimeSpan.FromMilliseconds(delay);
    }
    public List<string> All { get; set; } = new List<string>();

    public async Task<List<string>> GetBeautySalonsAsync()
    {
        await Task.Delay(_delay);
        return await Task.FromResult(All);
    }
}