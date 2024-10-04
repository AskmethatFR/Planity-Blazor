namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public interface IBeautySalonGateway
{
    Task<List<BeautySalon>> GetBeautySalonsAsync();
    Task<bool> PostBeautySalon(BeautySalon beautySalon);
}