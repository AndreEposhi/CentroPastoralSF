namespace CentroPastoralSF.Core.Requests.Usuario
{
    public class PerfilUsuarioRequest
    {
        public int Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}