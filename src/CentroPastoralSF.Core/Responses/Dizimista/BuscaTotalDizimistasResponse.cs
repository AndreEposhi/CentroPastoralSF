using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Dizimista
{
    public class BuscaTotalDizimistasResponse
    {
        [JsonPropertyName("dizimista")]
        public string Dizimista { get; set; } = null!;

        [JsonPropertyName("quantidade")]
        public int Quantidade { get; set; }
    }
}