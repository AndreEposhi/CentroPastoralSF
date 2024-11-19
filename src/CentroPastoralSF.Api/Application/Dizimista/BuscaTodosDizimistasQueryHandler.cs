using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.Dizimista;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public class BuscaTodosDizimistasQueryHandler(IDizimistaService dizimistaService)
        : IRequestHandler<BuscaTodosDizimistasQuery, Response<IEnumerable<BuscaTodosDizimistasResponse>>>
    {
        public async Task<Response<IEnumerable<BuscaTodosDizimistasResponse>>> Handle(BuscaTodosDizimistasQuery query, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var dizimistas = await dizimistaService.BuscarTodos();

                if (dizimistas is null || !dizimistas.Any())
                {
                    erros.Add("Não existe dizimistas.");

                    return new Response<IEnumerable<BuscaTodosDizimistasResponse>>(HttpStatusCode.NotFound, false, errors: erros);
                }


                return dizimistas.ToBuscaTodosDizimistasResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao buscar os dizimistas: {ex.Message}");

                return new Response<IEnumerable<BuscaTodosDizimistasResponse>>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}