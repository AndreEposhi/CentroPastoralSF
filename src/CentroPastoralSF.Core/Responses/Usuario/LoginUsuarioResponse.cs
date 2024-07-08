using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Usuario
{
    public class LoginUsuarioResponse
    {
        [JsonPropertyName("login")]
        public string Login { get; set; } = null!;

        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;
    }
}