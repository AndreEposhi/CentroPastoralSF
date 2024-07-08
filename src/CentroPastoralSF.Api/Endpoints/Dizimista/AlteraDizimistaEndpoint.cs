using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Api.Converters;
using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using MediatR;

namespace CentroPastoralSF.Api.Endpoints.Dizimista
{
    public class AlteraDizimistaEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapPut("/", Handler)
                .WithName("Dizimista: Alterar")
                .WithSummary("Altera os dados do dizimista")
                .WithDescription("Altera os dados do dizimista")
                .WithOrder(3)
                .Produces<Response<AlteraDizimistaResponse>>();
        }

        public static async Task<IResult> Handler(IMediator mediator, AlteraDizimistaRequest request)
        {
            var dizimista = await mediator.Send(request.ToAlteraDizimistaCommand());

            return dizimista.ToResult();
        }
    }
}