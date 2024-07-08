namespace CentroPastoralSF.Domain.ValueObjects
{
    public class Nomeacao
    {
        public Nomeacao(string nome, string sobrenome)
        {
            Validar(nome, sobrenome);

            Nome = nome;
            Sobrenome = sobrenome;
        }

        protected Nomeacao()
        {
        }

        public string Nome { get; private set; } = null!;
        public string Sobrenome { get; private set; } = null!;
        public Validacao Validacao { get; private set; } = null!;

        private void Validar(string nome, string sobrenome)
        {
            Validacao = new Validacao();

            if (string.IsNullOrWhiteSpace(nome))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Nome é obrigatório."));
            }

            if (nome.Length < 3)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Nome deve conter no mínimo 3 caracteres."));
            }

            if (string.IsNullOrWhiteSpace(sobrenome))
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Sobrenome é obrigatório."));
            }

            if (sobrenome.Length < 3)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Sobrenome deve conter no mínimo 3 caracteres."));
            }
        }
    }
}