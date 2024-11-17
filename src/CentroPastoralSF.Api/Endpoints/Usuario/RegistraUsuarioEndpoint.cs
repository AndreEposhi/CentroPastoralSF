using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Api.Converters;
using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class RegistraUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", Handler)
                .WithName("RegistraUsuario")
                .WithSummary("Registra um novo usuário")
                .WithDescription("Registra um novo usuário")
                .WithOrder(1)
                .Produces<Response<RegistraUsuarioResponse>>();
        }

        private async static Task<IResult> Handler(IMediator mediator, RegistraUsuarioRequest request)
        {
            var usuario = await mediator.Send(request.ToRegistraUsuarioCommand());

            return usuario.ToResult($"v1/usuario/{usuario?.Data?.Email}");
        }
    }
}