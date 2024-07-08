using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Linq.Dynamic.Core;


namespace CentroPastoralSF.WebApp.Pages.Usuario
{
    public partial class ListaUsuario
    {
        const string TextoPaginacao = "Mostrando página {0} de {1} <b>(total {2} registros)</b>";
        [Inject] UsuarioService UsuarioService { get; set; } = null!;
        [Inject] DialogService Dialog { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        IEnumerable<BuscaTodosUsuariosResponse> usuarios = null!;
        IQueryable<BuscaTodosUsuariosResponse> todosUsuarios = null!;
        IList<BuscaTodosUsuariosResponse> usuariosSelecionados = null!;

        bool isLoading = false;

        int count = 0;

        protected override void OnInitialized()
        {
            usuariosSelecionados = new List<BuscaTodosUsuariosResponse>();
        }

        private async Task BuscarUsuarios(LoadDataArgs args)
        {
            try
            {
                isLoading = true;

                await Task.Yield();

                if (count == 0)
                {
                    var buscaTodosUsuariosResponse = await UsuarioService.BuscarTodos();

                    todosUsuarios = buscaTodosUsuariosResponse.Data.AsQueryable();
                }

                var usuariosQuery = todosUsuarios;

                if (!string.IsNullOrWhiteSpace(args.Filter))
                {
                    usuariosQuery = usuariosQuery.Where(args.Filter);
                }

                if (!string.IsNullOrWhiteSpace(args.OrderBy))
                {
                    usuariosQuery = usuariosQuery.OrderBy(args.OrderBy);
                }

                count = usuariosQuery.Count();

                usuarios = usuariosQuery.Skip(args.Skip.Value).Take(args.Top.Value).ToList();
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
            finally
            {
                isLoading = false;

                StateHasChanged();
            }
        }

        private async Task Excluir()
        {
            if (!usuariosSelecionados.Any())
            {
                ExibirMensagem(NotificationSeverity.Info, "Selecione um usuário.");

                return;
            }

            var id = usuariosSelecionados.FirstOrDefault().Id;

            var excluiu = await Dialog.OpenAsync<ExcluiUsuario>("Excluir usuário", 
                new Dictionary<string, object> { { "Id", id } }, 
                new DialogOptions 
                {
                    Width = "450px;",
                    Height = "200px;",
                    Resizable = false,
                    ShowClose = false
                });

            if (excluiu)
            {
                await RecarregarLista();
            }
        }

        private async Task RecarregarLista()
        {
            var nenhumFiltro = new LoadDataArgs
            {
                Filter = string.Empty,
                OrderBy = string.Empty,
                Top = 8,
                Skip= 0
            };

            await BuscarUsuarios(nenhumFiltro);
        }

        private void SelecionarUsuario()
        {
            usuariosSelecionados = usuarios.Take(1).ToList();
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}
