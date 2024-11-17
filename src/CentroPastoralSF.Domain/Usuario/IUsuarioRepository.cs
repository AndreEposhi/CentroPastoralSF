namespace CentroPastoralSF.Domain.Usuario
{
    public interface IUsuarioRepository
    {
        Task<Usuario> Adicionar(Usuario usuario);
        Task<IQueryable<Usuario?>> BuscarTodos();
        Task<Usuario?> BuscarPorEmail(string email);
        Task Atualizar(Usuario usuario);
        Task Remover(Usuario usuario);
        Task<Usuario?> BuscarPorId(int id);
        Task<bool> VerificarSeJaExiste(string email);
    }
}