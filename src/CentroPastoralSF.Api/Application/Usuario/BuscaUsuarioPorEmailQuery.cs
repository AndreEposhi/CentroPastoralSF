using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public record BuscaUsuarioPorEmailQuery(string Email) : IRequest<Response<BuscaUsuarioPorEmailResponse>>;
}
