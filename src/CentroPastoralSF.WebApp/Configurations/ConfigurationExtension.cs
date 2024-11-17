using Blazor.SubtleCrypto;
using CentroPastoralSF.WebApp.Services.Cep;
using CentroPastoralSF.WebApp.Services.Dizimista;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using Radzen;

namespace CentroPastoralSF.WebApp.Configurations
{
    public static class ConfigurationExtension
    {
        public static void AddRadzenConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddRadzenComponents();
        }

        public static void AddServiceConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddTransient<UsuarioService>();
            builder.Services.AddTransient<DizimistaService>();
            builder.Services.AddTransient<CepService>();
            builder.Services.AddSingleton(service => new Core.Services.CryptoService(service.GetService<IJSRuntime>()));
        }

        public static void AddHttpClientConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddHttpClient(WebConfiguration.HttpClientName, options =>
            {
                options.BaseAddress = new Uri(WebConfiguration.BackendUrl);
            });
        }

        public static void AddSubtleCryptoConfiguration(this WebAssemblyHostBuilder builder)
        {
            builder.Services.AddSubtleCrypto(opt =>
                opt.Key = "ELE9xOyAyJHCsIPLMbbZHQ7pVy7WUlvZ60y5WkKDGMSw5xh5IM54kUPlycKmHF9VGtYUilglL8iePLwr"
                );
        }
    }
}