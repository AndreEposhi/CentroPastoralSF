using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Domain.Dizimista;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Dizimista
{
    public class IncluiDizimistaCommandHandler(IDizimistaService dizimistaService) : IRequestHandler<IncluiDizimistaCommand, Response<IncluiDizimistaResponse>>
    {
        public async Task<Response<IncluiDizimistaResponse>> Handle(IncluiDizimistaCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var dizimista = command.ToDizimista();

                var validacaoCampos = dizimista.Validacao.Erros.Where(d => d.TipoValidacao == Domain.TipoValidacao.Campo);
                var validacaoNegocios = dizimista.Validacao.Erros.Where(d => d.TipoValidacao == Domain.TipoValidacao.Negocio);


                if (validacaoCampos.Any())
                {
                    foreach (var erro in validacaoCampos)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return dizimista.ToIncluiDizimistaResponse(HttpStatusCode.BadRequest, false, errors: erros);
                }

                if (validacaoNegocios.Any())
                {
                    foreach (var erro in validacaoNegocios)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return dizimista.ToIncluiDizimistaResponse(HttpStatusCode.UnprocessableEntity, false, errors: erros);
                }


                dizimista = await dizimistaService.Incluir(dizimista);

                return dizimista.ToIncluiDizimistaResponse(HttpStatusCode.Created, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao adicionar o dizimista: {ex.Message}");

                //Todo: Provisoriamente, pois, no .Net 9 tem o InternalServerErro, no formato de IResult -> TypedREsults.InternalServerError();
                return new Response<IncluiDizimistaResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
