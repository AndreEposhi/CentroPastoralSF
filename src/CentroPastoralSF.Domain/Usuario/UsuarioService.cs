using CentroPastoralSF.Domain.ValueObjects;

namespace CentroPastoralSF.Domain.Usuario
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            if (await ValidarSeEstaCadastrado(usuario))
            {
                return usuario;
            }

            return await usuarioRepository.Adicionar(usuario);
        }

        public async Task Alterar(Usuario usuario, Nomeacao nome, Email login, string senha)
        {
            if (await ValidarSeEmailEstaCadastradoEmOutroPerfil(usuario, login.Endereco))
            {
                return;
            }

            usuario.Atualizar(nome, login, senha);

            if (usuario.Validacao.Erros.Any())
            {
                return;
            }

            await usuarioRepository.Atualizar(usuario);
        }

        public async Task<Usuario?> BuscarPorEmail(string email)
        {
            return await usuarioRepository.BuscarPorEmail(email);
        }

        public async Task<Usuario?> BuscarPorId(int id)
        {
            return await usuarioRepository.BuscarPorId(id);
        }

        public async Task<IQueryable<Usuario?>> BuscarTodos()
        {
            return await usuarioRepository.BuscarTodos();
        }

        public async Task Remover(Usuario usuario)
        {
            await usuarioRepository.Remover(usuario);
        }

        public Usuario ValidarLogin(Usuario usuario, string email, string senha)
        {
            usuario.ValidarLogin(email, senha);

            return usuario;
        }

        private async Task<bool> ValidarSeEstaCadastrado(Usuario usuario)
        {
            var usuarioCadastrado = await usuarioRepository.BuscarPorEmail(usuario.Login.Endereco);

            if (usuarioCadastrado != null)
            {
                usuario.Validacao.AdicionarErro(new Erro(TipoValidacao.Negocio, "Usuário já está cadastrado."));
            }

            return usuarioCadastrado != null;
        }

        private async Task<bool> ValidarSeEmailEstaCadastradoEmOutroPerfil(Usuario usuario, string email)
        {
            var usuarioCadastrado = await usuarioRepository.VerificarSeJaExiste(email);

            if (usuarioCadastrado)
            {
                usuario.Validacao.AdicionarErro(new Erro(TipoValidacao.Negocio, "Este e-mail já está cadastrado."));
            }

            return usuarioCadastrado;
        }
    }
}