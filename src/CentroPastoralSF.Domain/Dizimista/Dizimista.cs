using CentroPastoralSF.Domain.ValueObjects;

namespace CentroPastoralSF.Domain.Dizimista
{
    public class Dizimista : Entity
    {
        public Dizimista(Nomeacao nome, Telefone telefone, Endereco endereco, Email email, DateTime dataNascimento, int usuarioId)
        {
            Validar(nome, telefone, endereco, email, dataNascimento, usuarioId);

            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            Email = email;
            DataNascimento = dataNascimento;
            UsuarioId = usuarioId;
        }
        protected Dizimista()
        { }

        public Nomeacao Nome { get; private set; } = null!;
        public Telefone Telefone { get; private set; } = null!;
        public Endereco Endereco { get; private set; } = null!;
        public Email Email { get; private set; } = null!;
        public DateTime DataNascimento { get; set; }
        public int UsuarioId { get; private set; }
        public Validacao Validacao { get; private set; } = null!;

        public void Atualizar(Nomeacao nome, Telefone telefone, Endereco endereco, Email email, DateTime dataNascimento, int usuarioId)
        {
            Validar(nome, telefone, endereco, email, dataNascimento, usuarioId);

            Nome = nome;
            Telefone = telefone;
            Endereco = endereco;
            Email = email;
            DataNascimento = dataNascimento;
            UsuarioId = usuarioId;
        }
        private void Validar(Nomeacao nome, Telefone telefone, Endereco endereco, Email email, DateTime dataNascimento, int usuarioId)
        {
            Validacao = new Validacao();

            if (!nome.Validacao.EValido)
            {
                foreach (var erro in nome.Validacao.Erros)
                {
                    Validacao.AdicionarErro(erro);
                }
            }

            if (!telefone.Validacao.EValido)
            {
                foreach (var erro in telefone.Validacao.Erros)
                {
                    Validacao.AdicionarErro(erro);
                }
            }

            if (!endereco.Validacao.EValido)
            {
                foreach (var erro in endereco.Validacao.Erros)
                {
                    Validacao.AdicionarErro(erro);
                }
            }

            if (!email.Validacao.EValido)
            {
                foreach (var erro in email.Validacao.Erros)
                {
                    Validacao.AdicionarErro(erro);
                }
            }

            if (dataNascimento == DateTime.MinValue || dataNascimento == DateTime.MaxValue)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, 
                    "Data de nascimento é invalida."));
            }

            if (usuarioId <= 0)
            {
                Validacao.AdicionarErro(new Erro(TipoValidacao.Campo, 
                    "Usuário é inválido."));
            }
        }
    }
}