namespace CentroPastoralSF.Core.Requests.Dizimista
{
    public class IncluiDizimistaRequest
    {
        public string Nome { get; set; } = null!;
        public string Sobrenome { get; set; } = null!;
        public string Ddd { get; set; } = null!;
        public string Telefone { get; set; } = null!;
        public string Logradouro { get; set; } = null!;
        public string Numero { get; set; } = null!;
        public string Complemento { get; set; } = null!;
        public string Bairro { get; set; } = null!;
        public string Municipio { get; set; } = null!;
        public string UF { get; set; } = null!;
        public string Cep { get; set; } = null!;
        public int UsuarioId { get; set; } = 4;
        public string Email { get; set; } = null!;
        public DateTime DataNascimento { get; set; }
    }
}