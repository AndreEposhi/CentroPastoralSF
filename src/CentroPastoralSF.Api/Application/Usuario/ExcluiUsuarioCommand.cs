using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public record ExcluiUsuarioCommand(int Id) : IRequest<Response<ExcluiUsuarioResponse>>;
}
