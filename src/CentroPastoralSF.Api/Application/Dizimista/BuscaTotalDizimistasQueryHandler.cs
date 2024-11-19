using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.Dizimista;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public class BuscaTotalDizimistasQueryHandler(IDizimistaService dizimistaService) :
        IRequestHandler<BuscaTotalDizimistasQuery, Response<BuscaTotalDizimistasResponse>>
    {
        public async Task<Response<BuscaTotalDizimistasResponse>> Handle(BuscaTotalDizimistasQuery request, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var totalDizimistas = await dizimistaService.BuscarTotal();

                return new Response<BuscaTotalDizimistasResponse>(HttpStatusCode.OK, true,
                    new BuscaTotalDizimistasResponse 
                    {
                        Dizimista = "Dizimistas",
                        Quantidade = totalDizimistas
                    });
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao buscar o total dos dizimistas: {ex.Message}");

                return new Response<BuscaTotalDizimistasResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
