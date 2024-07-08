using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Cep
{
    public class CepResponse
    {
        [JsonConstructor]
        public CepResponse()
        { }

        [JsonPropertyName("cep")]
        public string Cep { get; set; } = null!;

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; } = null!;

        [JsonPropertyName("complemento")]
        public string Complemento { get; set; } = null!;

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; } = null!;

        [JsonPropertyName("localidade")]
        public string Municipio { get; set; } = null!;

        [JsonPropertyName("uf")]
        public string UF { get; set; } = null!;
    }
}