using CentroPastoralSF.Core.Requests.Cep;
using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Responses.Cep;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.Core.Utilities;
using CentroPastoralSF.WebApp.Services.Cep;
using CentroPastoralSF.WebApp.Services.Dizimista;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Dizimista
{
    public partial class AlteraDizimista
    {
        AlteraDizimistaRequest dizimista = null!;

        bool alterou = false;

        [Parameter] public int Id { get; set; }
        [Inject] CepService CepService { get; set; } = null!;
        [Inject] DizimistaService DizimistaService { get; set; } = null!;
        [Inject] DialogService Dialog { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;
        protected override async Task OnInitializedAsync()
        {
            dizimista = new AlteraDizimistaRequest();

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

        private async Task Confirmar()
        {
            alterou = false;

            try
            {
                dizimista.Id = Id;
                var alteraDizimistaResponse = await DizimistaService.Alterar(dizimista);

                if (alteraDizimistaResponse is not null && !alteraDizimistaResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", alteraDizimistaResponse.Errors);

                    alterou = false;
                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                alterou = true;
                ExibirMensagem(NotificationSeverity.Success, "Dizimista alterado com sucesso.");
            }
            catch (Exception ex)
            {
                alterou = false;
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private async Task BuscarCep()
        {
            if (string.IsNullOrWhiteSpace(dizimista.Cep) || dizimista.Cep.Length < 8)
            {
                return;
            }

            var cep = dizimista.Cep.RemoverFormatacaoCep();

            try
            {
                var cepResult = await CepService.BuscarCep(new BuscaCepRequest { Cep = cep });

                if (cepResult is not null)
                {
                    PreencherEndereco(cepResult);
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem(NotificationSeverity.Error, ex.Message);
            }
        }

        private void PreencherEndereco(CepResponse cep)
        {
            dizimista.Logradouro = cep.Logradouro;
            dizimista.Complemento = cep.Complemento;
            dizimista.Bairro = cep.Bairro;
            dizimista.Municipio = cep.Municipio;
            dizimista.UF = cep.UF;
        }

        private void Cancelar()
        {
            Dialog.Close(alterou);
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