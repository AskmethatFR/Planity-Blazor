namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public interface IBeautySalonGateway
{
    Task<List<BeautySalon>> GetBeautySalonsAsync();
    bool PostBeautySalon(BeautySalon beautySalon);
}