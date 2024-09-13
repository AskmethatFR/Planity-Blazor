namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public class AllBeautySalonQuery
{
    private readonly IBeautySalonGateway _beautySalonGateway;

    public AllBeautySalonQuery(IBeautySalonGateway beautySalonGateway)
    {
        _beautySalonGateway = beautySalonGateway;
    }

    public Task<List<BeautySalon>> Handle()
    {
        return _beautySalonGateway.GetBeautySalonsAsync();
    }
}