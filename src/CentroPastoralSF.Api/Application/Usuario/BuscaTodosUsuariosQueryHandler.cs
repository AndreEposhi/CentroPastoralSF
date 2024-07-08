using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Domain.Usuario;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public class BuscaTodosUsuariosQueryHandler(IUsuarioService usuarioService) 
        : IRequestHandler<BuscaTodosUsuariosQuery, Response<IEnumerable<BuscaTodosUsuariosResponse>>>
    {
        public async Task<Response<IEnumerable<BuscaTodosUsuariosResponse>>> Handle(BuscaTodosUsuariosQuery query, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var usuarios = await usuarioService.BuscarTodos();

                if (usuarios is null || !usuarios.Any())
                {
                    erros.Add("Não existe usuários.");

                    return new Response<IEnumerable<BuscaTodosUsuariosResponse>>(HttpStatusCode.NoContent, false, errors: erros);
                }

                return usuarios.ToBuscaTodosUsuariosResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao buscar os usuários: {ex.Message}");

                return new Response<IEnumerable<BuscaTodosUsuariosResponse>>(HttpStatusCode.InternalServerError, false, errors: erros);
            }
        }
    }
}