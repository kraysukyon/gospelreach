using GospelReachCapstone;
using GospelReachCapstone.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<FirebaseAuthenticationService>();
builder.Services.AddScoped<AuthState>();
builder.Services.AddScoped<FirestoreService>();
builder.Services.AddScoped<ChordsFormatterService>();

await builder.Build().RunAsync();
