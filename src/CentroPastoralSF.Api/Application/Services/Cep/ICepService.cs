using CentroPastoralSF.Core.Requests.Cep;
using CentroPastoralSF.Core.Responses;
using CentroPastoralSF.Core.Responses.Cep;

namespace CentroPastoralSF.Api.Application.Services.Cep
{
    public interface ICepService
    {
        Task<Response<CepResponse>> BuscarCep(BuscaCepRequest request);
    }
}