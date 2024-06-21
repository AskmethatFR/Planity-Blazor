using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PlanityBlazor.BlazorApp;
using PlanityBlazor.BlazorApp.BeautySalonContext.GetBeautySalonsQuery;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<AllBeautySalonQuery>();
builder.Services.AddScoped<IBeautySalonGateway>(_ =>
{
    var inMemorySalonGateway = new InMemoryBeautySalonGateway();
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