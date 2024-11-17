using CentroPastoralSF.Api.Application.Services.Cep;
using CentroPastoralSF.Core.Services;
using CentroPastoralSF.Domain.Dizimista;
using CentroPastoralSF.Domain.Usuario;
using CentroPastoralSF.Infraestructure.Data;
using CentroPastoralSF.Infraestructure.Data.Dizimista;
using CentroPastoralSF.Infraestructure.Data.Usuario;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;
using System.Reflection;

namespace CentroPastoralSF.Api.Configurations
{
    public static class ConfigurationExtension
    {
        public static void AddDocumentation(this WebApplicationBuilder builder)
        {
            // Add services to the container.
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options => options.CustomSchemaIds(cfg => cfg.FullName));
        }

        public static void AddDataContext(this WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            builder.Services.AddDbContext<CentroPastoralSFContext>(options =>
            {
                options.UseSqlServer(connectionString,
                    options => options.MigrationsAssembly("CentroPastoralSF.Infraestructure"));
            });
        }

        public static void AddCrossOrigin(this WebApplicationBuilder builder)
        {
            ApiConfiguration.CorsPolicyName = builder.Configuration.GetValue<string>("CorsPolicyName") ?? string.Empty;
            ApiConfiguration.BackendUrl = builder.Configuration.GetValue<string>("BackendUrl") ?? string.Empty;
            ApiConfiguration.FrontendUrl = builder.Configuration.GetValue<string>("FrontendUrl") ?? string.Empty;
            ApiConfiguration.ViaCepUrl = builder.Configuration.GetValue<string>("ViaCepUrl") ?? string.Empty;

            builder.Services.AddCors(options => options.AddPolicy(ApiConfiguration.CorsPolicyName,
                policy => policy.WithOrigins(
                                                            [
                                                            ApiConfiguration.BackendUrl,
                                                            ApiConfiguration.FrontendUrl,
                                                            ApiConfiguration.ViaCepUrl
                                                            ])
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            ));
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IDizimistaService, DizimistaService>();
            builder.Services.AddTransient<IDizimistaRepository, DizimistaRepository>();
            builder.Services.AddTransient<IUsuarioService, UsuarioService>();
            builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddTransient<ICepService, CepService>();
            builder.Services.AddSingleton(service => new CryptoService(service.GetService<IJSRuntime>()));
        }

        public static void AddMediator(this WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(options =>
                options.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        }

        public static void AddHttpClientConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.ViaCepHttpClientName = builder.Configuration.GetValue<string>("ViaCepHttpClientName") ?? string.Empty;
            ApiConfiguration.ViaCepBaseUrl = builder.Configuration.GetValue<string>("ViaCepBaseUrl") ?? string.Empty;

            builder.Services.AddHttpClient(ApiConfiguration.ViaCepHttpClientName, options =>
            {
                options.BaseAddress = new Uri(ApiConfiguration.ViaCepBaseUrl);
            });
        }

        public static void AddEncryptorConfiguration(this WebApplicationBuilder builder)
        {
            ApiConfiguration.AlgorithmKey = builder.Configuration.GetValue<string>("AlgorithmKey") ?? string.Empty;
            ApiConfiguration.AlgorithmIV = builder.Configuration.GetValue<string>("AlgorithmIV") ?? string.Empty;
        }
    }
}