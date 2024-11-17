using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Api.Converters;
using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class PerfilUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/", Handler)
                .WithName("PerfilUsuario")
                .WithSummary("Atualiza o perfil do usuário.")
                .WithDescription("Atualiza o perfil do usuário.")
                .WithOrder(4)
                .Produces<Response<PerfilUsuarioResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, PerfilUsuarioRequest request)
        {
            var usuarioResponse = await mediator.Send(request.ToPerfilUsuarioCommand());

            return usuarioResponse.ToResult();
        }
    }
}
