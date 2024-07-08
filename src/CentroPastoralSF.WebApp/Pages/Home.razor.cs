using Microsoft.AspNetCore.Components;

namespace CentroPastoralSF.WebApp.Pages
{
    public partial class Home
    {
        bool sidebarExpanded = true;
        bool listaDizimista = false;
        bool listaUsuario = false;

        [Parameter] public string Email { get; set; } = null!;
        [Parameter] public string Nome { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;

        protected override void OnInitialized()
        {
            base.OnInitialized();
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
    }
}
