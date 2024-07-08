using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Domain.Usuario;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public class RegistraUsuarioCommandHandler(IUsuarioService usuarioService) : IRequestHandler<RegistraUsuarioCommand, Response<RegistraUsuarioResponse>>
    {
        public async Task<Response<RegistraUsuarioResponse>> Handle(RegistraUsuarioCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var usuario = command.ToUsuario();

                var validacaoCampos = usuario.Validacao.Erros.Where(e => e.TipoValidacao == Domain.TipoValidacao.Campo);
                var validacaoNegocios = usuario.Validacao.Erros.Where(e => e.TipoValidacao == Domain.TipoValidacao.Negocio);

                if (validacaoCampos.Any())
                {
                    foreach (var erro in validacaoCampos)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return usuario.ToRegistraUsuarioResponse(HttpStatusCode.BadRequest, false, erros);
                }

                if (validacaoNegocios.Any())
                {
                    foreach (var erro in validacaoNegocios)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return usuario.ToRegistraUsuarioResponse(HttpStatusCode.UnprocessableEntity, false, erros);
                }

                usuario = await usuarioService.Adicionar(usuario);

                return usuario.ToRegistraUsuarioResponse(HttpStatusCode.Created, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu uma falha ao registrar o usuário: {ex.Message}");

                return new Response<RegistraUsuarioResponse>(HttpStatusCode.InternalServerError, false, errors: erros);
            }
        }
    }
}