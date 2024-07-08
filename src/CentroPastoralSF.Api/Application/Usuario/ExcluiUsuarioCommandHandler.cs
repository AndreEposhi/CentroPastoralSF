using CentroPastoralSF.Api.Application.Converters;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Domain.Usuario;
using MediatR;
using System.Net;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public class ExcluiUsuarioCommandHandler(IUsuarioService usuarioService) : IRequestHandler<ExcluiUsuarioCommand, Response<ExcluiUsuarioResponse>>
    {
        public async Task<Response<ExcluiUsuarioResponse>> Handle(ExcluiUsuarioCommand command, CancellationToken cancellationToken)
        {
            var erros = new List<string>();

            try
            {
                var usuario = await usuarioService.BuscarPorId(command.Id);

                if (usuario is null)
                {
                    erros.Add("Usuário não existe.");

                    return new Response<ExcluiUsuarioResponse>(HttpStatusCode.NotFound, false, errors: erros);
                }

                await usuarioService.Remover(usuario);

                return usuario.ToExcluiUsuarioResponse(HttpStatusCode.OK, true);
            }
            catch (Exception ex)
            {
                erros.Add($"Ocorreu um erro ao exclui o usuário: {ex.Message}");

                return new Response<ExcluiUsuarioResponse>(HttpStatusCode.UnprocessableEntity, false, errors: erros);
            }
        }
    }
}
