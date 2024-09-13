using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;

[FeatureState]
public record BeautySalonState
{
    public bool Progress { get; set; } = false;
    public List<BeautySalon> Salons { get; set; } = new List<BeautySalon>();
}