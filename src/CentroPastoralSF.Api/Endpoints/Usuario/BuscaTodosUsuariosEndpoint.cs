using CentroPastoralSF.Api.Application.Usuario;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Usuario
{
    public class BuscaTodosUsuariosEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", Handler)
                .WithName("BuscaTodosUsuarios")
                .WithSummary("Busca todos os usuários")
                .WithDescription("Busca todos os usuários")
                .WithOrder(2)
                .Produces<Response<IEnumerable<BuscaTodosUsuariosResponse>>>();
        }

        private async static Task<IResult> Handler(IMediator mediator)
        {
            var usuarios = await mediator.Send(new BuscaTodosUsuariosQuery());

            return usuarios.ToResult();
        }
    }
}