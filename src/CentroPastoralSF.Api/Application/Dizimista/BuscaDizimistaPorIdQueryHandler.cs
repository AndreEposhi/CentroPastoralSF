using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.Dizimista;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public class BuscaDizimistaPorIdQueryHandler(IDizimistaService dizimistaService) : IRequestHandler<BuscaDizimistaPorIdQuery, Response<BuscaDizimistaPorIdResponse>>
    {
        public async Task<Response<BuscaDizimistaPorIdResponse>> Handle(BuscaDizimistaPorIdQuery query, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var dizimista = await dizimistaService.BuscarPorId(query.Id);

                if (dizimista is null)
                {
                    erros.Add("Dizimista não existe.");

                    return new Response<BuscaDizimistaPorIdResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                return dizimista.ToBuscaDizimistaporIdResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao buscar o dizimista: {ex.Message}");

                return new Response<BuscaDizimistaPorIdResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
