using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Core.Utilities;
using CentroPastoralSF.WebApp.Services.Dizimista;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Dizimista
{
    public partial class DetalheDizimista
    {
        DizimistaResponse dizimista = null!;

        [Parameter] public int Id { get; set; }
        [Inject] DizimistaService DizimistaService { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            dizimista = new DizimistaResponse();

            try
            {
                await BuscarDizimista();
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private async Task BuscarDizimista()
        {
            var buscaDizimistaPorIdResponse = await DizimistaService.BuscarPorId(new BuscaDizimistaPorIdRequest { Id = Id });

            if (buscaDizimistaPorIdResponse is not null && !buscaDizimistaPorIdResponse.Success)
            {
                var mensagemErro = string.Join("<br/>", buscaDizimistaPorIdResponse.Errors);

                ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                return;
            }

            CarregarDadosDizimista(buscaDizimistaPorIdResponse?.Data);
        }

        private void CarregarDadosDizimista(BuscaDizimistaPorIdResponse buscaDizimistaPorIdResponse)
        {
            dizimista.Nome = buscaDizimistaPorIdResponse.Nome;
            dizimista.Sobrenome = buscaDizimistaPorIdResponse.Sobrenome;
            dizimista.Ddd = buscaDizimistaPorIdResponse.Ddd;
            dizimista.Telefone = buscaDizimistaPorIdResponse.Telefone.FormatarTelefone();
            dizimista.Cep = buscaDizimistaPorIdResponse.Cep.FormatarCep();
            dizimista.Logradouro = buscaDizimistaPorIdResponse.Logradouro;
            dizimista.Numero = buscaDizimistaPorIdResponse.Numero;
            dizimista.Complemento = buscaDizimistaPorIdResponse.Complemento;
            dizimista.Bairro = buscaDizimistaPorIdResponse.Bairro;
            dizimista.Municipio = buscaDizimistaPorIdResponse.Municipio;
            dizimista.UF = buscaDizimistaPorIdResponse.UF;
            dizimista.Email = buscaDizimistaPorIdResponse.Email;
            dizimista.DataNascimento = buscaDizimistaPorIdResponse.DataNascimento;
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}