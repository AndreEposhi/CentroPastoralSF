using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Api.Converters;
using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class LoginUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/login", Handler)
                .WithName("LoginUsuario")
                .WithSummary("Login do usuário")
                .WithDescription("Login do usuário")
                .WithOrder(3)
                .Produces<Response<LoginUsuarioResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, LoginUsuarioRequest request)
        {
            var usuario = await mediator.Send(request.ToLoginUsuarioQuery());

            return usuario.ToResult();
        }
    }
}