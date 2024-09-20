using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp.Components;

namespace PlanityBlazorApp.BlazorAppTest;

public class CreateBeautySalonPageTest : FixtureBunit
{
    [Fact]
    public void ShouldRenderCreateBeautySalonPage()
    {
        var cut = RenderComponent<PlanityBlazor.BlazorApp.Pages.CreateBeautySalon>();

        // Act
        var markup = cut.Markup;

        markup.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldContainCreateBeautySalonComponent()
    {
        var cut = RenderComponent<PlanityBlazor.BlazorApp.Pages.CreateBeautySalon>();

        var createBeautySalonComponent = () => cut.FindComponent<CreateBeautySalonComponent>();

        createBeautySalonComponent.Should().NotThrow();
    }
}