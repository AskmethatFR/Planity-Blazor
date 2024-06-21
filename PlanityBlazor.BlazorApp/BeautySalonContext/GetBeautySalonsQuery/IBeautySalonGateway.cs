namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public interface IBeautySalonGateway
{
    Task<List<string>> GetBeautySalonsAsync();
}