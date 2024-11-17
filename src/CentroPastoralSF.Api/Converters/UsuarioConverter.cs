using CentroPastoralSF.Api.Application.Usuario;
using CentroPastoralSF.Core.Requests.Usuario;

namespace CentroPastoralSF.Api.Converters
{
    public static class UsuarioConverter
    {
        public static RegistraUsuarioCommand ToRegistraUsuarioCommand(this RegistraUsuarioRequest request)
        {
            return new RegistraUsuarioCommand(request.Nome, request.Sobrenome, request.Login, request.Senha);
        }

        public static PerfilUsuarioCommand ToPerfilUsuarioCommand(this PerfilUsuarioRequest request)
        {
            return new PerfilUsuarioCommand(request.Id, request.Nome, request.Sobrenome, 
                request.Login, request.Senha);
        }

        public static LoginUsuarioQuery ToLoginUsuarioQuery(this LoginUsuarioRequest request)
        {
            return new LoginUsuarioQuery(request.Email, request.Senha);
        }
    }
}