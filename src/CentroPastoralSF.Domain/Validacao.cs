namespace CentroPastoralSF.Domain
{
    public enum TipoValidacao
    {
        Nenhum,
        Campo,
        Negocio
    }

    public class Validacao
    {
        public Validacao() { }

        private IList<Erro> erros = new List<Erro>();
        public bool EValido { get { return !erros.Any(); } }
        public IReadOnlyCollection<Erro> Erros => erros.AsReadOnly();

        public void AdicionarErro(Erro erro)
        {
            erros.Add(erro);
        }

        public void LimparErros()
        {
            erros.Clear();
        }
    }

    public class Erro
    {
        protected Erro() { }   
        public Erro(TipoValidacao tipoValidacao, string mensagemErro)
        {
            TipoValidacao = tipoValidacao;
            MensagemErro = mensagemErro;
        }
        public TipoValidacao TipoValidacao { get; private set; }
        public string MensagemErro { get; private set; } = null!;
    };
}