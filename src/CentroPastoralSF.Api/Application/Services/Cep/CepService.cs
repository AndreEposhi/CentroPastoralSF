using CentroPastoralSF.Api.Configurations;
using CentroPastoralSF.Core.Requests.Cep;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Cep;

namespace CentroPastoralSF.Api.Application.Services.Cep
{
    public class CepService(IHttpClientFactory httpClientFactory) : ICepService
    {
        private readonly HttpClient client = httpClientFactory.CreateClient(ApiConfiguration.ViaCepHttpClientName);

        public async Task<Response<CepResponse>> BuscarCep(BuscaCepRequest request)
        {
            Response<CepResponse> response = null!;
            var erros = new List<string>();


            try
            {
                if (string.IsNullOrWhiteSpace(request.Cep))
                {
                    erros.Add("Cep é obrigatório");

                    response = new Response<CepResponse>(System.Net.HttpStatusCode.BadRequest, false, errors: erros);

                    return response;
                }

                var cep = await client.GetFromJsonAsync<CepResponse>($"{request.Cep}/json");

                if (cep is not null)
                {
                    response = new Response<CepResponse>(System.Net.HttpStatusCode.OK, true, cep);

                    return response;
                }

                response = new Response<CepResponse>(System.Net.HttpStatusCode.NoContent, false);

                return response;
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);

                response = new Response<CepResponse>(System.Net.HttpStatusCode.InternalServerError, false, errors:  erros);

                return response;
            }
        }
    }
}