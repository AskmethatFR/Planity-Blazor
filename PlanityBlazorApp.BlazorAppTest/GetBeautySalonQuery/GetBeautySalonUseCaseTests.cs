using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest.GetBeautySalonQuery;

public class GetBeautySalonUseCaseTests
{
    private readonly IServiceProvider _serviceProvider;

    public GetBeautySalonUseCaseTests()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));

        services.AddScoped<IBeautySalonGateway, InMemoryBeautySalonGateway>();
        
        _serviceProvider = services.BuildServiceProvider();
        
        var store = _serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();
    }

    [Fact]
    public void ShouldHaveNoBeautySalons()
    {
        var beautyState = _serviceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautyState.Value.Salons.Should().BeEmpty();
        beautyState.Value.Progress.Should().BeFalse();
    }
    
    [Fact]
    public void ShouldHaveExpectedBeautySalons()
    {
        var expectedBeautySalons = new List<string>
        {
            "BeautySalon1",
            "BeautySalon2",
            "BeautySalon3",
            "BeautySalon4",
        };
        
        GivenBeautySalonInGateway(expectedBeautySalons);

        WhenBeautySalonAreGet();

        var beautyState = _serviceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautyState.Value.Salons.Should().BeEquivalentTo(expectedBeautySalons);
        beautyState.Value.Progress.Should().BeFalse();
    }

    private void WhenBeautySalonAreGet()
    {
       var dispatcher = _serviceProvider.GetRequiredService<IDispatcher>();
       dispatcher.Dispatch(new GetSalonsAction());
    }

    private void GivenBeautySalonInGateway(List<string> beautySalons)
    {
        var inMemoryBeautySalonGateway = _serviceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway;
        inMemoryBeautySalonGateway.All = beautySalons;
    }
}
