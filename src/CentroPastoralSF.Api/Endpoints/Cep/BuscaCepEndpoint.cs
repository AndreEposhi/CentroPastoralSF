using CentroPastoralSF.Api.Application.Services.Cep;
using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Requests.Cep;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Cep;

namespace CentroPastoralSF.Api.Endpoints.Cep
{
    public class BuscaCepEndpoint : IEndpoint
    {
        public static void Map(IEndpointRouteBuilder app)
        {
            app.MapGet("/{cep}", Handler)
                .WithName("Cep: Buscar")
                .WithSummary("Busca o cep na serviço Via Cep")
                .WithDescription("Busca o cep na serviço Via Cep")
                .WithOrder(1)
                .Produces<Response<CepResponse>>();
        }

        public async static Task<IResult> Handler(ICepService cepService, string cep)
        {
            var cepResult = await cepService.BuscarCep(new BuscaCepRequest { Cep = cep });

            return cepResult.ToResult();
        }
    }
}