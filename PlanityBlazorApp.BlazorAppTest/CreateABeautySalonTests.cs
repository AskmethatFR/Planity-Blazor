using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest;

public class CreateABeautySalonTests
{
    private readonly IServiceProvider _serviceProvider;

    public CreateABeautySalonTests()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));

        services.AddScoped<IBeautySalonGateway, InMemoryBeautySalonGateway>();
        
        _serviceProvider = services.BuildServiceProvider();
        
        var store = _serviceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();
    }


    [Fact]
    public void ShouldCreateABeautySalon()
    {
        var (beautySalonGateway, sut, beautySalonState) = InMemoryBeautySalonGateway();
        var expectedBeautySalon = "A beauty salon";

        sut.Execute(expectedBeautySalon);

        beautySalonState.Value.Salons.Should().Contain(expectedBeautySalon);
        beautySalonGateway.All.Should().Contain(expectedBeautySalon);
    }
    
    private (InMemoryBeautySalonGateway beautySalonGateway, CreateABeautySalon sut, IState<BeautySalonState>) InMemoryBeautySalonGateway()
    {
        var beautySalonState = _serviceProvider.GetRequiredService<IState<BeautySalonState>>();
        InMemoryBeautySalonGateway beautySalonGateway = (_serviceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway)!;
        var sut = new CreateABeautySalon(beautySalonState, beautySalonGateway);
        return (beautySalonGateway, sut, beautySalonState);
    }

    [Fact]
    public void ShouldNotCreateABeautySalon()
    {
        var (beautySalonGateway, sut, beautySalonState) = InMemoryBeautySalonGateway();
        beautySalonGateway.PostReturnsError = true;
        
        sut.Execute("A beauty salon");
        
        beautySalonState.Value.Salons.Should().NotContain("A beauty salon");
    }
}