using CentroPastoralSF.Core.Requests.Usuario;
using CentroPastoralSF.Core.Responses.Usuario;
using CentroPastoralSF.Core.Services;
using CentroPastoralSF.WebApp.Services.Usuario;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Perfil
{
    public partial class Perfil
    {
        PerfilUsuarioRequest perfilUsuario = null!;

        [Parameter] public string Email { get; set; } = null!;
        [Inject] NavigationManager Navigation { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;
        [Inject] UsuarioService UsuarioService { get; set; } = null!;
        [Inject] CryptoService CryptoService { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            perfilUsuario = new PerfilUsuarioRequest();

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

            await CarregarDadosUsuario(buscaUsuarioPorEmailResponse?.Data);
        }

        private async Task Confirmar()
        {
            try
            {
                var senhaDigitada = perfilUsuario.Senha;

                perfilUsuario.Senha = await CryptoService.Encrypt(perfilUsuario.Senha);

                var perfilUsuarioResponse = await UsuarioService.Atualizar(perfilUsuario);

                if (perfilUsuarioResponse is not null && !perfilUsuarioResponse.Success)
                {
                    var mensagemErro = string.Empty;

                    foreach (var erro in perfilUsuarioResponse.Errors)
                    {
                        mensagemErro = string.Join("<br/>", erro);
                    }

                    perfilUsuario.Senha = senhaDigitada;

                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                ExibirMensagem(NotificationSeverity.Success, "Perfil alterado com sucesso.");

                Cancelar();
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private async Task CarregarDadosUsuario(BuscaUsuarioPorEmailResponse buscaUsuarioPorEmailResponse)
        {
            perfilUsuario.Id = buscaUsuarioPorEmailResponse.Id;
            perfilUsuario.Nome = buscaUsuarioPorEmailResponse.Nome;
            perfilUsuario.Sobrenome = buscaUsuarioPorEmailResponse.Sobrenome;
            perfilUsuario.Login = buscaUsuarioPorEmailResponse.Login;
            perfilUsuario.Senha = await DescriptografarSenha( buscaUsuarioPorEmailResponse.Senha);
        }

        private async Task<string> DescriptografarSenha(string senha)
        {
            return await CryptoService.Decrypt(senha);
        }

        private void Cancelar()
        {
            Navigation.NavigateTo($"/home/{perfilUsuario?.Login}/{perfilUsuario?.Nome}");
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}