using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Core.Services;
using CentroPastoralSF.Domain.Usuario;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public class LoginUsuarioQueryHandler(IUsuarioService usuarioService, CryptoService cryptoService1) :
        IRequestHandler<LoginUsuarioQuery, Response<LoginUsuarioResponse>>
    {
        public async Task<Response<LoginUsuarioResponse>> Handle(LoginUsuarioQuery query, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var usuario = await usuarioService.BuscarPorEmail(query.Email);

                if (usuario is null)
                {
                    erros.Add("Usuário não existe.");

                    return new Response<LoginUsuarioResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                usuario = await usuarioService.ValidarLogin(usuario, query.Email, query.Senha);

                if (!usuario.Validacao.EValido)
                {
                    foreach (var erro in usuario.Validacao.Erros)
                    {
                        erros.Add(erro.MensagemErro);
                    }

                    return usuario.ToLoginUsuarioResponse(HttpStatusCode.UnprocessableEntity, false, erros);
                }

                return usuario.ToLoginUsuarioResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao logar: {ex.Message}");

                return new Response<LoginUsuarioResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}