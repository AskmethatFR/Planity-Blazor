using Bunit;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.Components;
using static PlanityBlazor.BlazorApp.ViewModelState;

namespace PlanityBlazorApp.BlazorAppTest;

public class BeautySalonListComponentTest : TestContext
{
    [Theory, InlineData("Nothing here")]
    public void InitialBeautySalonComponentShouldBeEmpty(string message)
    {
        var component = this.CreateSut(new() { Status = Nothing, NothingMessage = message });

        Verify(component, message);
    }

    [Theory, InlineData("Loading salons...")]
    public void LoadingBeautySalonComponentShouldDisplayLoadingMessage(string message)
    {
        var component = this.CreateSut(new() { Status = Progress, LoadingMessage = message });

        Verify(component, message);
    }

    [Theory, InlineData("Error while loading")]
    public void ErrorBeautySalonComponentShouldDisplayErrorMessage(string message)
    {
        var component = this.CreateSut(new() { Status = Error, ErrorMessage = message });

        Verify(component, message);
    }

    [Fact]
    public void BeautySalonsComponentDisplaySalons()
    {
        var component = this.CreateSut(new() { Status = Completed, BeautySalons = ["Salon 1", "Salon 2"] });
        Verify(component, "Salon 1", "Salon 2");
    }

    private IRenderedComponent<BeautySalonListComponent> CreateSut(BeautySalonsViewModel viewModel) =>
        this.RenderComponent<BeautySalonListComponent>(option => option.Add(p => p.ViewModel, viewModel));

    private static void Verify(IRenderedComponent<BeautySalonListComponent> component, params string[] content) =>
        component.Markup.Should().ContainAll(content);
}
