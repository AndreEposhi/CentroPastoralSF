using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Usuario
{
    public class RegistraUsuarioResponse
    {
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;

        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; } = null!;

        [JsonPropertyName("email")]
        public string Email { get; set; } = null!;
    }
}