using System.Text.RegularExpressions;

namespace CentroPastoralSF.Domain.ValueObjects
{
    public class Email
    {
        public Email(string endereco)
        {
            Validar(endereco);

            Endereco = endereco;
        }

        protected Email()
        {
        }
        public string Endereco { get; private set; } = null!;

        public Validacao Validacao { get; private set; } = null!;

        private void Validar(string endereco)
        {
            Validacao = new Validacao();

            if (string.IsNullOrWhiteSpace(endereco))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Email é obrigatório."));
            }

            var emailExpressao = new Regex("^\\S+@\\S+\\.\\S+$");

            if (!emailExpressao.IsMatch(endereco))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Email é inválido."));
            }
        }
    }
}