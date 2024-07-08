using CentroPastoralSF.Api.Application.Dizimista;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Dizimista
{
    public class BuscaDizimistaPorIdEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{id}", Handler)
                .WithName("Buscar: Dizimista")
                .WithSummary("Busca um dizmista pelo identificador")
                .WithDescription("Busca um dizimista pelo identificador")
                .WithOrder(4)
                .Produces<Response<BuscaDizimistaPorIdResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, int id)
        {
            var dizimista = await mediator.Send(new BuscaDizimistaPorIdQuery(id));

            return dizimista.ToResult();
        }
    }
}