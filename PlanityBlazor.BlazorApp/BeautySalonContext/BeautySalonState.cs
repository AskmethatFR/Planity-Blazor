using Fluxor;

namespace PlanityBlazor.BlazorApp.BeautySalonContext;

[FeatureState]
public record BeautySalonState
{
    public List<string> Salons { get; set; } = new List<string>();
    public bool Progress { get; set; } = false;
}