
using CentroPastoralSF.Api.Application.Dizimista;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Dizimista
{
    public class ExcluiDizimistaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapDelete("/{id}", Handler)
                .WithName("Dizimista: Excluir")
                .WithSummary("Exclui um dizimista")
                .WithDescription("Exclui um dizimista")
                .WithOrder(5)
                .Produces<Response<ExcluiDizimistaResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, int id)
        {
            var dizimista = await mediator.Send(new ExcluiDizimistaCommand(id));

            return dizimista.ToResult();
        }
    }
}
