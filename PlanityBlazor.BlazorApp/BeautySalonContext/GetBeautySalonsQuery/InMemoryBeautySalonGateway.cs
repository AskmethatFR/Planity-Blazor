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

    public async Task<List<BeautySalon>> GetBeautySalonsAsync()
    {
        await Task.Delay(_delay);
        return await Task.FromResult(new List<BeautySalon>(All.Select(x => new BeautySalon(x))));
    }

    public bool PostBeautySalon(BeautySalon beautySalon)
    {
        if (PostReturnsError)
        {
            return false;
        }

        All.Add(beautySalon.Name);
        return true;
    }
}