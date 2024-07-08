using CentroPastoralSF.Core.Requests.Cep;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Cep;
using CentroPastoralSF.WebApp.Configurations;
using System.Net.Http.Json;

namespace CentroPastoralSF.WebApp.Services.Cep
{
    public class CepService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<CepResponse> BuscarCep(BuscaCepRequest cep)
        {
            var cepResult = await client.GetFromJsonAsync<Response<CepResponse>>($"v1/cep/{cep.Cep}");

            return cepResult?.Data;
        }
    }
}