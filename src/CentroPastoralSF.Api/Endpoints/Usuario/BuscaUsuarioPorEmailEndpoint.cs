using CentroPastoralSF.Api.Application.Usuario;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class BuscaUsuarioPorEmailEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{email}", Handler)
                .WithName("Usuário: BuscarEmail")
                .WithSummary("Buscar usuário pelo e-mail")
                .WithDescription("Buscar usuário pelo e-mail")
                .WithOrder(6)
                .Produces<Response<BuscaUsuarioPorEmailResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, string email)
        {
            var usuario = await mediator.Send(new BuscaUsuarioPorEmailQuery( email));

            return usuario.ToResult();
        }
    }
}
