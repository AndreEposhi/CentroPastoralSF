using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public record LoginUsuarioQuery(string Email, string Senha) : IRequest<Response<LoginUsuarioResponse>>;
}