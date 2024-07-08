using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Dizimista
{
    public class BuscaDizimistaPorIdResponse
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;

        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; } = null!;

        [JsonPropertyName("ddd")]
        public string Ddd { get; set; } = null!;

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; } = null!;

        [JsonPropertyName("logradouro")]
        public string Logradouro { get; set; } = null!;

        [JsonPropertyName("numero")]
        public string Numero { get; set; } = null!;

        [JsonPropertyName("complemento")]
        public string? Complemento { get; set; }

        [JsonPropertyName("bairro")]
        public string Bairro { get; set; } = null!;

        [JsonPropertyName("municipio")]
        public string Municipio { get; set; } = null!;

        [JsonPropertyName("uf")]
        public string UF { get; set; } = null!;

        [JsonPropertyName("cep")]
        public string Cep { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;

        [JsonPropertyName("datanascimento")]
        public DateTime DataNascimento { get; set; }
    }
}