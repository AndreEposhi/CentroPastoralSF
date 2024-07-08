using CentroPastoralSF.Api.Application.Usuario;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class LoginUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{email}/{senha}", Handler)
                .WithName("Usuário: Login")
                .WithSummary("Login do usuário")
                .WithDescription("Login do usuário")
                .WithOrder(3)
                .Produces<Response<LoginUsuarioResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, string email, string senha)
        {
            var usuario = await mediator.Send(new LoginUsuarioQuery(email, senha));

            return usuario.ToResult();
        }
    }
}