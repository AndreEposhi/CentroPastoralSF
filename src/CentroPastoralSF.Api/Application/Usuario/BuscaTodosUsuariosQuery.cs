using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public record BuscaTodosUsuariosQuery : IRequest<Response<IEnumerable<BuscaTodosUsuariosResponse>>>;
}