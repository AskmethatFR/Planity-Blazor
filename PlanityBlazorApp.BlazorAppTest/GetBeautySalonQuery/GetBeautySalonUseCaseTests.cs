using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest.GetBeautySalonQuery;

public class GetBeautySalonUseCaseTests : Fixture
{
    [Fact]
    public void ShouldHaveNoBeautySalons()
    {
        var beautyState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautyState.Value.Salons.Should().BeEmpty();
        beautyState.Value.Progress.Should().BeFalse();
    }

    [Fact]
    public void ShouldHaveExpectedBeautySalons()
    {
        var expectedBeautySalons = new List<BeautySalon>
        {
            new BeautySalon("BeautySalon1"),
            new BeautySalon("BeautySalon2"),
            new BeautySalon("BeautySalon3"),
            new BeautySalon("BeautySalon4"),
        };

        var beautySalons = expectedBeautySalons.Select(s => s.Name).ToList();
        GivenBeautySalonInGateway(beautySalons);

        WhenBeautySalonAreGet();

        var beautyState = ServiceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautyState.Value.Salons.Should().BeEquivalentTo(expectedBeautySalons);
        beautyState.Value.Progress.Should().BeFalse();
    }

    private void WhenBeautySalonAreGet()
    {
        var dispatcher = ServiceProvider.GetRequiredService<IDispatcher>();
        dispatcher.Dispatch(new GetSalonsAction());
    }

    private void GivenBeautySalonInGateway(List<string> beautySalons)
    {
        var inMemoryBeautySalonGateway =
            ServiceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway;
        inMemoryBeautySalonGateway.All = beautySalons;
    }
}