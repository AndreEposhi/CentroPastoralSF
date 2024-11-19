using CentroPastoralSF.Core.Requests.Dizimista;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Dizimista;
using CentroPastoralSF.WebApp.Configurations;
using System.Net.Http.Json;

namespace CentroPastoralSF.WebApp.Services.Dizimista
{
    public class DizimistaService(IHttpClientFactory httpClientFactory)
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(WebConfiguration.HttpClientName);

        public async Task<Response<IEnumerable<BuscaTodosDizimistasResponse>>> BuscarTodos()
        {
            var dizimistas = await client.GetAsync("v1/dizimista");

            return await dizimistas.Content.ReadFromJsonAsync<Response<IEnumerable<BuscaTodosDizimistasResponse>>>();
        }

        public async Task<Response<IncluiDizimistaResponse>> Incluir(IncluiDizimistaRequest request)
        {
            var dizimista = await client.PostAsJsonAsync("v1/dizimista", request);

            return await dizimista.Content.ReadFromJsonAsync<Response<IncluiDizimistaResponse>>();
        }

        public async Task<Response<AlteraDizimistaResponse>> Alterar(AlteraDizimistaRequest request)
        {
            var dizimista = await client.PutAsJsonAsync($"v1/dizimista", request);

            return await dizimista.Content.ReadFromJsonAsync<Response<AlteraDizimistaResponse>>();
        }

        public async Task<Response<BuscaDizimistaPorIdResponse>> BuscarPorId(BuscaDizimistaPorIdRequest request)
        {
            var dizimista = await client.GetAsync($"v1/dizimista/{request.Id}");

            return await dizimista.Content.ReadFromJsonAsync<Response<BuscaDizimistaPorIdResponse>>();
        }

        public async Task<Response<ExcluiDizimistaResponse>> Excluir(ExcluiDizimistaRequest request)
        {
            var dizimista = await client.DeleteAsync($"v1/dizimista/{request.Id}");

            return await dizimista.Content.ReadFromJsonAsync<Response<ExcluiDizimistaResponse>>();
        }

        public async Task<Response<BuscaTotalDizimistasResponse>> BuscarTotal()
        {
            try
            {
                var dizimistas = await client.GetAsync("v1/dizimista");

                return await dizimistas.Content.ReadFromJsonAsync<Response<BuscaTotalDizimistasResponse>>();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}