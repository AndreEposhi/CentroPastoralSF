using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using MediatR;

namespace CentroPastoralSF.Api.Application.Usuario
{
    public record PerfilUsuarioCommand(int Id, string Nome, string Sobrenome, string Login, string Senha) 
        : IRequest<Response<PerfilUsuarioResponse>>;
}
