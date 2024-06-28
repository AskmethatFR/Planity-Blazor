using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;
using PlanityBlazor.BlazorApp.Pages;

namespace PlanityBlazorApp.BlazorAppTest;

public class GetBeautySalonUseCase
{
    private readonly IServiceProvider _serviceProvider;

    public GetBeautySalonUseCase()
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
        var beautyState = _serviceProvider.GetRequiredService<IState<MyState>>();
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

        var beautyState = _serviceProvider.GetRequiredService<IState<MyState>>();
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