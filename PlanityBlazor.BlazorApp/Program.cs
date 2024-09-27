using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext;
using PlanityBlazor.BlazorApp.BeautySalonContext.CreateBeautySalon;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;
using PlanityBlazor.BlazorApp.Shared.Reactive;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseReduxDevTools();
});
builder.Services.AddScoped(typeof(IReactiveSelector<BeautySalonState, BeautySalonsViewModel>),
    typeof(BeautySalonsSelector));

builder.Services.AddScoped(typeof(IReactiveSelector<BeautySalonState, CreationError>),
    typeof(IsBeautySalonsCreationError));


builder.Services.AddScoped(typeof(AppSelector<,>));
builder.Services.AddScoped<AllBeautySalonQuery>();
builder.Services.AddScoped<IBeautySalonGateway>(_ =>
{
    var inMemorySalonGateway = new InMemoryBeautySalonGateway(0);
    inMemorySalonGateway.All = new List<string>()
    {
        "Salon 1",
        "Salon 2",
        "Salon 3",
        "Salon 4",
        "Salon 5",
    };

    return inMemorySalonGateway;
});


await builder.Build().RunAsync();

public partial class Program
{
}