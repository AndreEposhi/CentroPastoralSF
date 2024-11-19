using CentroPastoralSF.Api.Application.Dizimista;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Dizimista
{
    public class BuscaTotalDizimistasEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/total", Handler)
                .WithName("BuscarTotal")
                .WithSummary("Busca o total de dizimistas")
                .WithDescription("Busca o total de dizimistas")
                .WithOrder(6)
                .Produces<Response<BuscaTotalDizimistasResponse>>();
        }

        private async static Task<IResult> Handler(IMediator mediator)
        {
            var dizimistas = await mediator.Send(new BuscaTotalDizimistasQuery());

            return dizimistas.ToResult();
        }
    }
}
