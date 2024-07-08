using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Domain.Usuario;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public class BuscaUsuarioPorEmailQueryHandler(IUsuarioService usuarioService)
        : IRequestHandler<BuscaUsuarioPorEmailQuery, Response<BuscaUsuarioPorEmailResponse>>
    {
        public async Task<Response<BuscaUsuarioPorEmailResponse>> Handle(BuscaUsuarioPorEmailQuery query, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var usuario = await usuarioService.BuscarPorEmail(query.Email);

                if (usuario is null)
                {
                    erros.Add("Usuário não existe.");

                    return new Response<BuscaUsuarioPorEmailResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                return usuario.ToBuscarUsuarioPorEmailResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao buscar o usuário: {ex.Message}");

                return new Response<BuscaUsuarioPorEmailResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
