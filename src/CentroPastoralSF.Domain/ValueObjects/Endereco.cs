namespace CentroPastoralSF.Domain.ValueObjects
{
    public class Endereco
    {
        public Endereco(string logradouro, string numero, string complemento, string bairro, string municipio, string uf, string cep)
        {
            Validar(logradouro, numero, bairro, municipio, uf, cep);

            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Municipio = municipio;
            UF = uf;
            Cep = cep;
        }

        protected Endereco() { }

        public string Logradouro { get; private set; } = null!;
        public string Numero { get; private set; } = null!;
        public string? Complemento { get; private set; }
        public string Bairro { get; private set; } = null!;
        public string Municipio { get; private set; } = null!;
        public string UF { get; private set; } = null!;
        public string Cep { get; private set; } = null!;
        public Validacao Validacao { get; private set; } = null!;

        private void Validar(string logradouro, string numero, string bairro, string municipio, string uf, string cep)
        {
            Validacao = new Validacao();

            ValidarLogradouro(logradouro);
            ValidarNumero(numero);
            ValidarBairro(bairro);
            ValidarMunicipio(municipio);
            ValidarUF(uf);
            ValidarCep(cep);
        }

        private void ValidarLogradouro(string logradouro)
        {
            if (string.IsNullOrWhiteSpace(logradouro))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Logradouro é obrigatório."));
            }

            if (logradouro.Length < 3)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Logradouro deve ter no mínimo 3 caracteres."));
            }
        }

        private void ValidarNumero(string numero)
        {
            if (string.IsNullOrWhiteSpace(numero))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Número do endereço é obrigatório."));
            }
        }

        private void ValidarBairro(string bairro)
        {
            if (string.IsNullOrWhiteSpace(bairro))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Bairro é obrigatório."));
            }

            if (bairro.Length < 3)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Bairro deve ter no mínimo 3 caracteres."));
            }
        }

        private void ValidarMunicipio(string municipio)
        {
            if (string.IsNullOrWhiteSpace(municipio))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Municípío é obrigatório."));
            }

            if (municipio.Length < 3)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Município deve ter no mínimo 3 caracteres."));
            }
        }

        private void ValidarUF(string uf)
        {
            if (string.IsNullOrWhiteSpace(uf))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "UF é obrigatória."));
            }

            if (uf.Length != 2)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "UF é inválida."));
            }
        }

        private void ValidarCep(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "CEP é obrigatório."));
            }

            if (cep.Length != 8)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "CEP é inválido."));
            }
        }
    }
}