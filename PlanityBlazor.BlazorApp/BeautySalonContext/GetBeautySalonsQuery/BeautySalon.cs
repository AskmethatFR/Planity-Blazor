namespace PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

public class BeautySalon
{
    public BeautySalon(string name)
    {
        Name = name;
    }

    public string Name { get; private set; }
}