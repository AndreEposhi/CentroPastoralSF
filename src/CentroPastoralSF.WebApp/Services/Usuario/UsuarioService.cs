using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.WebApp.Configurations;
using System.Net.Http.Json;

namespace CentroPastoralSF.WebApp.Services.Usuario
{
    public class UsuarioService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        //Todo: Acessar os endpoints do usuário via HttpClient

        public async Task<Response<RegistraUsuarioResponse>> Registrar(RegistraUsuarioRequest request)
        {
            var usuario = await client.PostAsJsonAsync("v1/usuario", request);

            return await usuario.Content.ReadFromJsonAsync<Response<RegistraUsuarioResponse>>();
        }

        public async Task<Response<IEnumerable<BuscaTodosUsuariosResponse>>> BuscarTodos()
        {
            var usuarios = await client.GetAsync("v1/usuario");

            return await usuarios.Content.ReadFromJsonAsync<Response<IEnumerable<BuscaTodosUsuariosResponse>>>();
        }

        public async Task<Response<BuscaUsuarioPorEmailResponse>> BuscarPorEmail(BuscarUsuarioPorEmailRequest request)
        {
            var usuario = await client.GetAsync($"v1/usuario/{request.Email}");

            return await usuario.Content.ReadFromJsonAsync<Response<BuscaUsuarioPorEmailResponse>>();
        }

        public async Task<Response<LoginUsuarioResponse>> Logar(LoginUsuarioRequest request)
        {
            //Todo: criptografar a senha
            var usuario = await client.GetAsync($"v1/usuario/{request.Email}/{request.Senha}");

            return await usuario.Content.ReadFromJsonAsync<Response<LoginUsuarioResponse>>();
        }

        public async Task<Response<PerfilUsuarioResponse>> Atualizar(PerfilUsuarioRequest request)
        {
            var usuario = await client.PutAsJsonAsync("v1/usuario", request);

            return await usuario.Content.ReadFromJsonAsync<Response<PerfilUsuarioResponse>>();
        }

        public async Task<Response<ExcluiUsuarioResponse>> Excluir(ExcluiUsuarioRequest request)
        {
            var usuario = await client.DeleteAsync($"v1/usuario/{request.Id}");

            return await usuario.Content.ReadFromJsonAsync<Response<ExcluiUsuarioResponse>>();
        }
    }
}