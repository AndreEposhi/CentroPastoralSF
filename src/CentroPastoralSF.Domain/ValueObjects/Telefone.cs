namespace CentroPastoralSF.Domain.ValueObjects
{
    public class Telefone
    {
        public Telefone(string ddd, string numero)
        {
            Validar(ddd, numero);

            Ddd = ddd;
            Numero = numero;
        }

        protected Telefone() { }

        public string Ddd { get; private set; } = null!;
        public string Numero { get; private set; } = null!;
        public Validacao Validacao { get; private set; } = null!;

        private void Validar(string ddd, string numero)
        {
            Validacao = new Validacao();

            ValidarDdd(ddd);
            ValidarNumero(numero);
        }

        private void ValidarDdd(string ddd)
        {
            if (string.IsNullOrWhiteSpace(ddd))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "DDD é obrigatório."));
            }

            if (ddd.Length != 2)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "DDD é inválido."));
            }
        }

        private void ValidarNumero(string numero)
        {
            //numero = numero.Replace("-", "");

            if (string.IsNullOrWhiteSpace(numero))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Número do telefone é obrigatório."));
            }

            if (numero.Length < 7 || numero.Length > 9)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Número do telefone é inválido."));
            }
        }
    }
}