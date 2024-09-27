using Fluxor;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;

[FeatureState]
public record BeautySalonState
{
    public bool Progress { get; init; } = false;
    public List<BeautySalon> Salons { get; init; } = new List<BeautySalon>();
    public string Error { get; set; }
}