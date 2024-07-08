using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Login
{
    public partial class Login
    {
        [Inject] UsuarioService UsuarioService { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        private async Task Entrar(LoginArgs args)
        {
            try
            {
                var loginUsuarioRequest = new LoginUsuarioRequest
                {
                    Email = args.Username,
                    Senha = args.Password
                };

                var loginUsuarioResponse = await UsuarioService.Logar(loginUsuarioRequest);

                if (loginUsuarioResponse is not null && !loginUsuarioResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", loginUsuarioResponse.Errors);

                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                Navigation.NavigateTo($"/home/{loginUsuarioResponse?.Data?.Login}/{loginUsuarioResponse?.Data?.Nome}");
                //Navigation.NavigateTo($"/home");
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private void Registrar()
        {
            Navigation.NavigateTo("/registra");
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}