using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Perfil
{
    public partial class Perfil
    {
        PerfilUsuarioRequest usuario = null!;

        [Parameter] public string Email { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;
        [Inject] UsuarioService UsuarioService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            usuario = new PerfilUsuarioRequest();

            try
            {
                await BuscarUsuario();
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private async Task BuscarUsuario()
        {
            var buscarUsuarioPorEmailRequest = new BuscarUsuarioPorEmailRequest
            {
                Email = Email
            };

            var buscaUsuarioPorEmailResponse = await UsuarioService.BuscarPorEmail(buscarUsuarioPorEmailRequest);

            if (buscaUsuarioPorEmailResponse is not null && !buscaUsuarioPorEmailResponse.Success)
            {
                var mensagemErro = string.Join("<br/>", buscaUsuarioPorEmailResponse.Errors);

                ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                return;
            }

            CarregarDadosUsuario(buscaUsuarioPorEmailResponse?.Data);
        }

        private void CarregarDadosUsuario(BuscaUsuarioPorEmailResponse buscaUsuarioPorEmailResponse)
        {
            usuario.Id = buscaUsuarioPorEmailResponse.Id;
            usuario.Nome = buscaUsuarioPorEmailResponse.Nome;
            usuario.Sobrenome = buscaUsuarioPorEmailResponse.Sobrenome;
            usuario.Login = buscaUsuarioPorEmailResponse.Login;
            usuario.Senha = buscaUsuarioPorEmailResponse.Senha;
        }

        private async Task Confirmar()
        {
            try
            {
                var perfilUsuarioResponse = await UsuarioService.Atualizar(usuario);

                if (perfilUsuarioResponse is not null && !perfilUsuarioResponse.Success)
                {
                    var mensagemErro = string.Empty;

                    foreach (var erro in perfilUsuarioResponse.Errors)
                    {
                        mensagemErro = string.Join("<br/>", erro);
                    }

                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                ExibirMensagem(NotificationSeverity.Success, "Perfil alterado com sucesso.");
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private void Cancelar()
        {
            Navigation.NavigateTo($"/home/{usuario?.Login}/{usuario?.Nome}");
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}