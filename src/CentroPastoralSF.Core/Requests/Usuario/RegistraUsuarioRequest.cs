namespace CentroPastoralSF.Core.Requests.Usuario
{
    public class RegistraUsuarioRequest
    {
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public string Login { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}