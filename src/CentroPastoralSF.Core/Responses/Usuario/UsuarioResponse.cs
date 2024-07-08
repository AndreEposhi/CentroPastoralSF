using System.Text.Json.Serialization;

namespace CentroPastoralSF.Core.Responses.Usuario
{
    public class UsuarioResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;
        [JsonPropertyName("sobrenome")]
        public string Sobrenome { get; set; } = null!;
        [JsonPropertyName("login")]
        public string Login { get; set; } = null!;
        [JsonPropertyName("senha")]
        public string Senha { get; set; } = null!;
    }
}