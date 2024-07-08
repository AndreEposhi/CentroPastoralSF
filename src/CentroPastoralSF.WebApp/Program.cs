using CentroPastoralSF.WebApp;
using CentroPastoralSF.WebApp.Configurations;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.AddRadzenConfiguration();
builder.AddHttpClientConfiguration();
builder.AddServiceConfiguration();
builder.AddSubtleCryptoConfiguration();

await builder.Build().RunAsync();