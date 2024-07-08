using CentroPastoralSF.Domain.ValueObjects;

namespace CentroPastoralSF.Domain.Usuario
{
    public class Usuario : Entity
    {
        public Usuario(Nomeacao nome, Email login, string senha)
        {
            Validar(nome, login, senha);

            Nome = nome;
            Login = login;
            Senha = senha;
        }
        protected Usuario()
        { }

        public Nomeacao Nome { get; private set; } = null!;
        public Email Login { get; private set; } = null!;
        public string Senha { get; private set; } = null!;
        public Validacao Validacao { get; private set; } = null!;

        public void Atualizar(Nomeacao nome, Email login, string senha)
        {
            Validar(nome, login, senha);

            Nome = nome;
            Login = login;
            Senha = senha;
        }

        public void ValidarLogin(string email, string senha)
        {
            Validacao = new Validacao();

            if (Login.Endereco != email || Senha != senha)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Negocio, "Login ou senha incorretos."));
            }
        }
        private void Validar(Nomeacao nome, Email login, string senha)
        {
            Validacao = new Validacao();

            if (!nome.Validacao.EValido)
            {
                foreach (var erro in nome.Validacao.Erros)
                {
                    Validacao.AdicionarErro(erro);
                }
            }

            if (!login.Validacao.EValido)
            {
                foreach (var erro in login.Validacao.Erros)
                {
                    Validacao.AdicionarErro(erro);
                }
            }

            ValidarSenha(Validacao, senha);
        }
       
        private void ValidarSenha(Validacao validacao, string senha)
        {
            if (string.IsNullOrWhiteSpace(senha))
            {
                validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Senha é obrigatória."));
            }

            if (senha.Length < 3)
            {
                validacao.AdicionarErro(new Erro(TipoValidacao.Campo, "Senha deve conter no mínimo 3 caracteres."));
            }
        }
    }
}