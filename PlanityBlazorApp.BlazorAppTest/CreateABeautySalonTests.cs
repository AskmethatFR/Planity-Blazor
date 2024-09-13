using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest;

public class CreateABeautySalonTests : Fixture
{
    [Fact]
    public void ShouldCreateABeautySalon()
    {
        var sut = ServiceProvider.GetRequiredService<IDispatcher>();
        var expectedBeautySalon = "A beauty salon";

        sut.Dispatch(new CreateABeautySalonAction(expectedBeautySalon));

        var beautySalonGateway =
            (ServiceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway)!;
        var beautySalonState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();

        beautySalonState.Value.Salons.Should().ContainEquivalentOf(new BeautySalon(expectedBeautySalon));
        beautySalonGateway.All.Should().Contain(expectedBeautySalon);
    }

    [Fact]
    public void ShouldNotCreateABeautySalon()
    {
        var sut = ServiceProvider.GetRequiredService<IDispatcher>();
        var expectedBeautySalon = "A beauty salon";
        var beautySalonGateway =
            (ServiceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway)!;

        beautySalonGateway.PostReturnsError = true;

        sut.Dispatch(new CreateABeautySalonAction(expectedBeautySalon));

        var beautySalonState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautySalonState.Value.Salons.Should().NotContain(new BeautySalon(expectedBeautySalon));
    }
}