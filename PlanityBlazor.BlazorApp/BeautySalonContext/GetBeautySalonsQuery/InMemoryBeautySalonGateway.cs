namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public class InMemoryBeautySalonGateway : IBeautySalonGateway
{
    private readonly TimeSpan _delay;

    public InMemoryBeautySalonGateway(int delay = 0)
    {
        _delay = TimeSpan.FromMilliseconds(delay);
    }
    public List<string> All { get; set; } = new List<string>();
    public bool PostReturnsError { get; set; }

    public async Task<List<string>> GetBeautySalonsAsync()
    {
        await Task.Delay(_delay);
        return await Task.FromResult(All);
    }

    public bool PostBeautySalon(string beautySalon)
    {
        if (PostReturnsError)
        {
            return false;
        }
        All.Add(beautySalon);
        return true;
    }
}