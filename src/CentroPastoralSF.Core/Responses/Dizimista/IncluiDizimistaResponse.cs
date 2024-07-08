using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Dizimista
{
    public class IncluiDizimistaResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;

        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; } = null!;
    }
}