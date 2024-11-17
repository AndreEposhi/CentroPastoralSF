using CentroPastoralSF.Api.Application.Usuario;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class ExcluiUsuarioEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", Handler)
                .WithName("ExcluiUsuario")
                .WithSummary("Exclui um usuário")
                .WithDescription("Exclui um usuário")
                .WithOrder(5)
                .Produces<Response<ExcluiUsuarioResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, int id)
        {
            var usuario = await mediator.Send(new ExcluiUsuarioCommand(id));

            return usuario.ToResult();
        }
    }
}
