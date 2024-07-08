using CentroPastoralSF.Api.Application.Usuario;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Domain.ValueObjects;
using System.Net;
using UsuarioDomain = CentroPastoralSF.Domain.Usuario.Usuario;

namespace CentroPastoralSF.Api.Application.Converters
{
    public static class UsuarioConverter
    {
        public static UsuarioDomain ToUsuario(this RegistraUsuarioCommand command)
        {
            var nome = new Nomeacao(command.Nome, command.Sobrenome);
            var email = new Email(command.Login);

            return new UsuarioDomain(nome, email, command.Senha);
        }

        public static UsuarioResponse ToUsuarioResponse(this UsuarioDomain usuario)
        {
            return new UsuarioResponse
            {
                Id = usuario.Id,
                Nome = usuario.Nome.Nome,
                Sobrenome = usuario.Nome.Sobrenome,
                Login = usuario.Login.Endereco,
                Senha = usuario.Senha
            };
        }

        public static Response<LoginUsuarioResponse> ToLoginUsuarioResponse(this UsuarioDomain usuario, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            LoginUsuarioResponse? data = null;

            if (success)
            {
                data = new LoginUsuarioResponse
                {
                    Login = usuario.Login.Endereco,
                    Nome = usuario.Nome.Nome
                };
            }

            return new Response<LoginUsuarioResponse>(statusCode, success, data, errors);
        }

        public static Response<IEnumerable<BuscaTodosUsuariosResponse>> ToBuscaTodosUsuariosResponse(this IQueryable<UsuarioDomain> usuarios, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            var data = new List<BuscaTodosUsuariosResponse>();

            if (success)
            {
                foreach (var usuario in usuarios)
                {
                    data.Add(new BuscaTodosUsuariosResponse
                    {
                        Id = usuario.Id,
                        Nome = usuario.Nome.Nome,
                        Sobrenome = usuario.Nome.Sobrenome,
                        Login = usuario.Login.Endereco
                    });
                }
            }

            return new Response<IEnumerable<BuscaTodosUsuariosResponse>>(statusCode, success, data?.AsEnumerable(), errors);
        }

        public static Response<RegistraUsuarioResponse> ToRegistraUsuarioResponse(this UsuarioDomain usuario, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            RegistraUsuarioResponse? data = null;

            if (success)
            {
                data = new RegistraUsuarioResponse
                {
                    Nome = usuario.Nome.Nome,
                    Sobrenome = usuario.Nome.Sobrenome,
                    Email = usuario.Login.Endereco
                };
            }

            return new Response<RegistraUsuarioResponse>(statusCode, success, data, errors);
        }

        public static Response<BuscaUsuarioPorEmailResponse> ToBuscarUsuarioPorEmailResponse(this UsuarioDomain usuario, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            BuscaUsuarioPorEmailResponse? data = null;

            if (success)
            {
                data = new BuscaUsuarioPorEmailResponse
                {
                    Id = usuario.Id,
                    Nome = usuario.Nome.Nome,
                    Sobrenome = usuario.Nome.Sobrenome,
                    Login = usuario.Login.Endereco,
                    Senha = usuario.Senha
                };
            }

            return new Response<BuscaUsuarioPorEmailResponse>(statusCode, success, data, errors);
        }

        public static Response<PerfilUsuarioResponse> ToPerfilUsuarioResponse(this UsuarioDomain usuario, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            return new Response<PerfilUsuarioResponse>(statusCode, success, null, errors);
        }

        public static Response<ExcluiUsuarioResponse> ToExcluiUsuarioResponse(this UsuarioDomain usuario, HttpStatusCode statusCode,
            bool success, IEnumerable<string>? errors = null)
        {
            return new Response<ExcluiUsuarioResponse>(statusCode, success, null, errors);
        }
    }
}