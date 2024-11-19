using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.WebApp.Services.Dizimista;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages
{
    public partial class Home
    {
        bool sidebarExpanded = true;
        bool listaDizimista = false;
        bool listaUsuario = false;

        IList<BuscaTotalDizimistasResponse> totalDizimistas = [];
        [Parameter] public string Email { get; set; } = null!;
        [Parameter] public string Nome { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;
        [Inject] DizimistaService DizimistaService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                //await PopularGraficoTotalDizimista();
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private async Task PopularGraficoTotalDizimista()
        {
            var buscaTotalDizimistasResponse = await DizimistaService.BuscarTotal();

            totalDizimistas.Add(buscaTotalDizimistasResponse?.Data);
        }

        private void Listar(TipoCadastro tipoCadastro)
        {
            if (tipoCadastro == TipoCadastro.Dizimista)
            {
                listaDizimista = true;
                listaUsuario = false;
            }
            else if (tipoCadastro == TipoCadastro.Usuario)
            {
                listaUsuario = true;
                listaDizimista = false;
            }
        }

        private void IrPerfil()
        {
            Navigation.NavigateTo($"perfil/{Email}");
        }

        private void Sair()
        {
            Navigation.NavigateTo("login");
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}
