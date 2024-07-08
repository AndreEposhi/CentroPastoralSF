using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.Dizimista;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public class ExcluiDizimistaCommandHandler(IDizimistaService dizimistaService) : IRequestHandler<ExcluiDizimistaCommand, Response<ExcluiDizimistaResponse>>
    {
        public async Task<Response<ExcluiDizimistaResponse>> Handle(ExcluiDizimistaCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var dizimista = await dizimistaService.BuscarPorId(command.Id);

                if (dizimista is null)
                {
                    erros.Add("Dizimista não existe.");

                    return new Response<ExcluiDizimistaResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                await dizimistaService.Remover(dizimista);

                return dizimista.ToExcluiDizimistaResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao excluir o dizimista: {ex.Message}");

                return new Response<ExcluiDizimistaResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
