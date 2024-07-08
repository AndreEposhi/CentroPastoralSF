using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Domain.Usuario;
using CentroPastoralSF.Domain.ValueObjects;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public class PerfilUsuarioCommandHandler(IUsuarioService usuarioService)
        : IRequestHandler<PerfilUsuarioCommand, Response<PerfilUsuarioResponse>>
    {
        public async Task<Response<PerfilUsuarioResponse>> Handle(PerfilUsuarioCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var usuario = await usuarioService.BuscarPorId(command.Id);

                if (usuario is null)
                {
                    erros.Add("Usuário não existe.");

                    return new Response<PerfilUsuarioResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                var nome = new Nomeacao(command.Nome, command.Sobrenome);
                var login = new Email(command.Login);

                usuario.Atualizar(nome, login, command.Senha);

                var validacoesCampos = usuario.Validacao.Erros.Where(u => u.TipoValidacao == Domain.TipoValidacao.Campo);
                var validacoesNegocios = usuario.Validacao.Erros.Where(u => u.TipoValidacao == Domain.TipoValidacao.Negocio);

                if (validacoesCampos.Any())
                {
                    foreach (var erro in validacoesCampos)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return usuario.ToPerfilUsuarioResponse(HttpStatusCode.BadRequest, false, errors: erros);
                }

                if (validacoesNegocios.Any())
                {
                    foreach (var erro in validacoesNegocios)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return usuario.ToPerfilUsuarioResponse(HttpStatusCode.UnprocessableEntity, false, errors: erros);
                }

                await usuarioService.Alterar(usuario);

                return usuario.ToPerfilUsuarioResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocoreu um erro ao atualizar o perfil: {ex.Message}");

                return new Response<PerfilUsuarioResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
