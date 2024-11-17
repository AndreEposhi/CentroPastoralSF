using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Services;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Login
{
    public partial class Login
    {
        [Inject] CryptoService CryptoService { get; set; } = null!;
        [Inject] UsuarioService UsuarioService { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        private async Task Entrar(LoginArgs args)
        {
            try
            {
                var senha = await CryptoService.Encrypt(args.Password);

                var loginUsuarioRequest = new LoginUsuarioRequest
                {
                    Email = args.Username,
                    Senha = senha
                };

                var loginUsuarioResponse = await UsuarioService.Logar(loginUsuarioRequest);

                if (loginUsuarioResponse is not null && !loginUsuarioResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", loginUsuarioResponse.Errors);

                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                Navigation.NavigateTo($"/home/{loginUsuarioResponse?.Data?.Login}/{loginUsuarioResponse?.Data?.Nome}");
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