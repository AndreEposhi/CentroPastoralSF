using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Dizimista
{
    public class BuscaTodosDizimistasResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;

        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; } = null!;

        [JsonPropertyName("ddd")]
        public string Ddd { get; set; } = null!;

        [JsonPropertyName("telefone")]
        public string Telefone { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}