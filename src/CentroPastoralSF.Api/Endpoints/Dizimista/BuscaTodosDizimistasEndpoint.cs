using CentroPastoralSF.Api.Application.Dizimista;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Dizimista
{
    public class BuscaTodosDizimistasEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/", Handler)
                .WithName("Dizmista: Buscar")
                .WithSummary("Busca todos os dizimistas")
                .WithDescription("Busca todos os dizimistas")
                .WithOrder(2)
                .Produces<Response<IEnumerable<BuscaTodosDizimistasResponse>>>();
        }

        private async static Task<IResult> Handler(IMediator mediator)
        {
            var dizimistas = await mediator.Send(new BuscarTodosDizimistasQuery());

            return dizimistas.ToResult();
        }
    }
}
