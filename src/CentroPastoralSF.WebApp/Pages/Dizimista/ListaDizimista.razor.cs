using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.WebApp.Services.Dizimista;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq.Dynamic.Core;

namespace CentroPastoralSF.WebApp.Pages.Dizimista
{
    public partial class ListaDizimista
    {
        const string TextoPaginacao = "Mostrando página {0} de {1} <b>(total {2} registros)</b>";
        [Inject] DizimistaService DizimistaService { get; set; } = null!;
        [Inject] DialogService Dialog { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        IEnumerable<BuscaTodosDizimistasResponse> dizimistas = null!;
        IQueryable<BuscaTodosDizimistasResponse> todosDizimistas = null!;
        IList<BuscaTodosDizimistasResponse> dizimistasSelecionados = null!;

        bool isLoading = false;
        bool? reload = false;

        int count = 0;
        protected override void OnInitialized()
        {
            dizimistasSelecionados = new List<BuscaTodosDizimistasResponse>();
        }

        private async Task BuscarDizimistas(LoadDataArgs args)
        {
            isLoading = true;

            await Task.Yield();

            try
            {
                if (count == 0)
                {
                    var buscaTodosDizimistasResponse = await DizimistaService.BuscarTodos();

                    todosDizimistas = buscaTodosDizimistasResponse?.Data?.AsQueryable();
                }

                var dizimistasQuery = todosDizimistas;

                if (!string.IsNullOrWhiteSpace(args.Filter))
                {
                    dizimistasQuery = dizimistasQuery.Where(args.Filter);
                }

                if (!string.IsNullOrWhiteSpace(args.OrderBy))
                {
                    dizimistasQuery = dizimistasQuery.OrderBy(args.OrderBy);
                }

                count = dizimistasQuery.Count();

                dizimistas = dizimistasQuery.Skip(args.Skip.Value).Take(args.Top.Value).ToList();
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error,  ex.Message);
            }
            finally
            {
                isLoading = false;

                StateHasChanged();
            }
        }

        private async Task Incluir()
        {
            var incluiu = await Dialog.OpenAsync<IncluiDizimista>("Incluir dizimista", null,
                new DialogOptions
                {
                    Width = "950px",
                    Resizable = false,
                    AutoFocusFirstElement = true,
                    ShowClose = false
                });

            if (incluiu)
            {
                await RecarregarLista();
            }
        }
        private async Task Editar()
        {
            if (!dizimistasSelecionados.Any())
            {
                ExibirMensagem(NotificationSeverity.Info, "Selecione o dizimista.");
                return;
            }

            var id = dizimistasSelecionados.FirstOrDefault().Id;

            var alterou = await Dialog.OpenAsync<AlteraDizimista>("Alterar dizimista",
                new Dictionary<string, object> { { "Id", id } },
                new DialogOptions
                {
                    Width = "950px",
                    Resizable = false,
                    AutoFocusFirstElement = true,
                    ShowClose = false
                });


            if (alterou)
            {
                await RecarregarLista();
            }
        }

        private async Task Excluir()
        {
            if (!dizimistasSelecionados.Any())
            {
                ExibirMensagem(NotificationSeverity.Info, "Selecione o dizimista.");
                return;
            }

            var id = dizimistasSelecionados.FirstOrDefault().Id;

            var excluiu = await Dialog.OpenAsync<ExcluiDizimista>("Excluir dizimista",
                new Dictionary<string, object> { { "Id", id } },
                new DialogOptions
                {
                    Width = "450px",
                    Height = "200px",
                    Resizable = false,
                    ShowClose = false
                });

            if (excluiu)
            {
                await RecarregarLista();
            }
        }

        private async Task MostrarDetalhe()
        {
            if (!dizimistasSelecionados.Any())
            {
                ExibirMensagem(NotificationSeverity.Info,  "Selecione o dizimista.");
                return;
            }

            var id = dizimistasSelecionados.FirstOrDefault().Id;

            await Dialog.OpenAsync<DetalheDizimista>("Detalhe dizimista",
                new Dictionary<string, object> { { "Id", id } },
                new DialogOptions
                {
                    Width = "850px",
                    Resizable = false
                });
        }

        private async Task RecarregarLista()
        {
            var semFiltro = new LoadDataArgs
            {
                Filter = string.Empty,
                OrderBy = string.Empty,
                Skip = 0,
                Top = 5
            };

            count = 0;

            await BuscarDizimistas(semFiltro);
        }

        private void SelecionarDizimista()
        {
            dizimistasSelecionados = dizimistas.Take(1).ToList();
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null,mensagem, duracao);
        }
    }
}