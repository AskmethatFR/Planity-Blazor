using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

namespace PlanityBlazorApp.BlazorAppTest;

public class Fixture
{
    public IServiceProvider ServiceProvider;

    public Fixture()
    {
        Init();
    }

    private void Init()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));

        services.AddScoped<IBeautySalonGateway, InMemoryBeautySalonGateway>();

        ServiceProvider = services.BuildServiceProvider();

        var store = ServiceProvider.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();
    }
}