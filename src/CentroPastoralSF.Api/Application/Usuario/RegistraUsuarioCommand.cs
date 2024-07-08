using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public record RegistraUsuarioCommand(string Nome, string Sobrenome, string Login, string Senha) : IRequest<Response<RegistraUsuarioResponse>>;
}