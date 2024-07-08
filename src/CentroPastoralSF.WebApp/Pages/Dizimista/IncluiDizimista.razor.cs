using CentroPastoralSF.Core.Requests.Cep;
using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Responses.Cep;
using CentroPastoralSF.Core.Utilities;
using CentroPastoralSF.WebApp.Services.Cep;
using CentroPastoralSF.WebApp.Services.Dizimista;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace CentroPastoralSF.WebApp.Pages.Dizimista
{
    public partial class IncluiDizimista
    {
        bool incluiu = false;

        IncluiDizimistaRequest dizimista = null!;
        [Inject] CepService CepService { get; set; } = null!;
        [Inject] DizimistaService DizimistaService { get; set; } = null!;
        [Inject] DialogService Dialog { get; set; } = null!;
        [Inject] NotificationService Notification { get; set; } = null!;
        protected override void OnInitialized()
        {
            dizimista = new IncluiDizimistaRequest();
        }

        private async Task Confirmar()
        {
            try
            {
                incluiu = false;

                var incluiDizimistaResponse = await DizimistaService.Incluir(dizimista);

                if (incluiDizimistaResponse is not null && !incluiDizimistaResponse.Success)
                {
                    var mensagemErro = string.Join("<br/>", incluiDizimistaResponse.Errors);

                    incluiu = false;
                    ExibirMensagem(NotificationSeverity.Error, mensagemErro);

                    return;
                }

                incluiu = true;
                ExibirMensagem(NotificationSeverity.Success, "Dizimista incluído com sucesso.");
            }
            catch (Exception ex)
            {
                incluiu = false;
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
            Dialog.Close(incluiu);
        }

        private void ExibirMensagem(NotificationSeverity severity, string mensagem, double duracao = 5000)
        {
            Notification.Notify(severity, null, mensagem, duracao);
        }
    }
}