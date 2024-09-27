using Bunit;
using Fluxor;
using Microsoft.Extensions.DependencyInjection;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;
using PlanityBlazor.BlazorApp.Shared.Reactive;

namespace PlanityBlazorApp.BlazorAppTest;

public class FixtureBunit : TestContext
{
    public FixtureBunit()
    {
        Init();
    }

    private void Init()
    {
        Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));

        Services.AddScoped<IBeautySalonGateway, InMemoryBeautySalonGateway>();
        Services.AddScoped(typeof(IReactiveSelector<BeautySalonState, BeautySalonsViewModel>),
            typeof(BeautySalonsSelector));

        Services.AddScoped(typeof(IReactiveSelector<BeautySalonState, CreationError>),
            typeof(IsBeautySalonsCreationError));

        Services.AddScoped(typeof(AppSelector<,>));

        var store = Services.GetRequiredService<IStore>();
        store.InitializeAsync().Wait();
    }
}