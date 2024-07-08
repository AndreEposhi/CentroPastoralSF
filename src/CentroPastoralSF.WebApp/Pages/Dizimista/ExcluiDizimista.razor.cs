using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.WebApp.Services.Dizimista;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Dizimista
{
    public partial class ExcluiDizimista
    {
        bool excluiu = false;

        [Parameter] public int Id { get; set; }
        [Inject] DizimistaService DizimistaService { get; set; } = null!;
        [Inject] DialogService Dialog { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        private async Task Confirmar()
        {
            try
            {
                var excluiDizimistaResponse = await DizimistaService.Excluir(new ExcluiDizimistaRequest { Id = Id });

                if (excluiDizimistaResponse is not null && !excluiDizimistaResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", excluiDizimistaResponse.Errors);

                    excluiu = false;
                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                excluiu = true;

                ExibirMensagem(NotificationSeverity.Success, "Dizimista excluído com sucesso.");
            }
            catch (Exception ex)
            {
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