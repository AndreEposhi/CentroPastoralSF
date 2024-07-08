using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Api.Converters;
using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Dizimista
{
    public class IncluiDizimistaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPost("/", Handler)
                .WithName("Dizimista: Incluir")
                .WithSummary("Inclui um novo dizimista")
                .WithDescription("Inclui um novo dizimista")
                .WithOrder(1)
                .Produces<Response<IncluiDizimistaResponse>>();
        }

        private static async Task<IResult> Handler(IMediator mediator, IncluiDizimistaRequest request)
        {
            var dizimista = await mediator.Send(request.ToIncluiDizimistaCommand());

            return dizimista.ToResult($"v1/dizimista/{dizimista.Data?.Id}");
        }
    }
}