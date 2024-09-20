using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

namespace PlanityBlazorApp.BlazorAppTest;

public class BeautySalonsPageTests : FixtureBunit
{
    [Fact]
    public void ShouldRenderBeautySalonsPage()
    {
        var cut = RenderComponent<PlanityBlazor.BlazorApp.Pages.BeautySalons>();

        // Act
        var markup = cut.Markup;

        markup.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void ShouldNavigateToCreateBeautySalonOnButtonClick()
    {
        var cut = RenderComponent<PlanityBlazor.BlazorApp.Pages.BeautySalons>();

        var button = cut.Find("#navigateToCreateBeautySalonButton");
        button.Click();

        var navigationManager = Services.GetRequiredService<NavigationManager>();
        navigationManager.Uri.Should().Be("http://localhost/CreateBeautySalon");
    }
}