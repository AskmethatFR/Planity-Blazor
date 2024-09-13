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
        var sut = _serviceProvider.GetRequiredService<IDispatcher>();
        var expectedBeautySalon = "A beauty salon";

        sut.Dispatch(new CreateABeautySalonAction(expectedBeautySalon));

        var beautySalonGateway =
            (_serviceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway)!;
        var beautySalonState = _serviceProvider.GetRequiredService<IState<BeautySalonState>>();

        beautySalonState.Value.Salons.Should().ContainEquivalentOf(new BeautySalon(expectedBeautySalon));
        beautySalonGateway.All.Should().Contain(expectedBeautySalon);
    }

    [Fact]
    public void ShouldNotCreateABeautySalon()
    {
        var sut = _serviceProvider.GetRequiredService<IDispatcher>();
        var expectedBeautySalon = "A beauty salon";
        var beautySalonGateway =
            (_serviceProvider.GetRequiredService<IBeautySalonGateway>() as InMemoryBeautySalonGateway)!;

        beautySalonGateway.PostReturnsError = true;

        sut.Dispatch(new CreateABeautySalonAction(expectedBeautySalon));

        var beautySalonState = _serviceProvider.GetRequiredService<IState<BeautySalonState>>();
        beautySalonState.Value.Salons.Should().NotContain(new BeautySalon(expectedBeautySalon));
    }
}