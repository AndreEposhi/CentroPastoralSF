using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Usuario
{
    public partial class ExcluiUsuario
    {
        bool excluiu = false;

        [Parameter] public int Id { get; set; }
        [Inject] UsuarioService UsuarioService { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;
        [Inject] DialogService Dialog { get; set; } = null!;
        private async Task Confirmar()
        {
            excluiu = false;

            try
            {
                var excluiUsuarioResponse = await UsuarioService.Excluir(new ExcluiUsuarioRequest { Id = Id });

                if (excluiUsuarioResponse is not null && !excluiUsuarioResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", excluiUsuarioResponse.Errors);

                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                excluiu = true;
                ExibirMensagem(NotificationSeverity.Success, "Usuário excluído com sucesso.");
            }
            catch (Exception ex)
            {
                excluiu = false;
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private void Cancelar()
        {
            Dialog.Close(excluiu);
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}