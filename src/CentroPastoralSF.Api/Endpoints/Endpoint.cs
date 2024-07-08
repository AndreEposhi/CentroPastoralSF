using CentroPastoralSF.Api.Endpoints.Cep;
using CentroPastoralSF.Api.Endpoints.Dizimista;
using CentroPastoralSF.Api.Endpoints.Usuario;

namespace CentroPastoralSF.Api.Endpoints
{
    public static class Endpoint
    {
        public static void MapEndpoints(this WebApplication app)
        {
            var endpoints = app.MapGroup("");

            endpoints.MapGroup("v1/dizimista")
                .WithTags("Dizimista")
                .MapEndpoint<IncluiDizimistaEndpoint>()
                .MapEndpoint<BuscaTodosDizimistasEndpoint>()
                .MapEndpoint<AlteraDizimistaEndpoint>()
                .MapEndpoint<BuscaDizimistaPorIdEndpoint>()
                .MapEndpoint<ExcluiDizimistaEndpoint>();

            endpoints.MapGroup("v1/usuario")
                .WithTags("Usuário")
                .MapEndpoint<RegistraUsuarioEndpoint>()
                .MapEndpoint<BuscaTodosUsuariosEndpoint>()
                .MapEndpoint<LoginUsuarioEndpoint>()
                .MapEndpoint<PerfilUsuarioEndpoint>()
                .MapEndpoint<ExcluiUsuarioEndpoint>()
                .MapEndpoint<BuscaUsuarioPorEmailEndpoint>();

            endpoints.MapGroup("v1/cep")
                .WithTags("Cep")
                .MapEndpoint<BuscaCepEndpoint>();
        }

        private static IEndpointRouteBuilder MapEndpoint<TEndpoint>(this IEndpointRouteBuilder app)
            where TEndpoint : IEndpoint
        {
            TEndpoint.Map(app);

            return app;
        }
    }
}
