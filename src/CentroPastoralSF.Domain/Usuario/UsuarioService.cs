namespace CentroPastoralSF.Domain.Usuario
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        public async Task<Usuario> Adicionar(Usuario usuario)
        {
            return await usuarioRepository.Adicionar(usuario);
        }

        public async Task Alterar(Usuario usuario)
        {
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
    }
}