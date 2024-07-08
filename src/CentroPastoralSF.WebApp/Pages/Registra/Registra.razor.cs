using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Registra
{
    public partial class Registra
    {
        RegistraUsuarioRequest usuario = null!;

        [Inject] UsuarioService UsuarioService { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        protected override void OnInitialized()
        {
            usuario = new RegistraUsuarioRequest();

            base.OnInitialized();
        }

        private async Task Registrar()
        {
            try
            {
                var registraUsuarioResponse = await UsuarioService.Registrar(usuario);

                if (registraUsuarioResponse is not null && !registraUsuarioResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", registraUsuarioResponse.Errors);

                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                ExibirMensagem(NotificationSeverity.Success, "Usuário registrado com sucesso.");
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private void Cancelar()
        {
            Navigation.NavigateTo("/login");
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}