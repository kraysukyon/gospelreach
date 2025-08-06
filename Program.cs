using GospelReachCapstone;
using GospelReachCapstone.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GospelReachCapstone.Models;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

//// Named client for the ASP.NET Core Web API
//builder.Services.AddHttpClient("ApiClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:5001/");
//});

builder.Services.AddSingleton<FirebaseConfigService>();
builder.Services.AddScoped<FirebaseAuthenticationService>();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<FirestoreService>();
builder.Services.AddScoped<ChordsFormatterService>();
builder.Services.AddScoped<GeneralFunctions>();
builder.Services.AddScoped<AppState>();

await builder.Build().RunAsync();
