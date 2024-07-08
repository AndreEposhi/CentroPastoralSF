namespace CentroPastoralSF.Domain.Usuario
{
    public interface IUsuarioService
    {
        Task<Usuario> Adicionar(Usuario usuario);
        Task<IQueryable<Usuario?>> BuscarTodos();
        Task<Usuario?> BuscarPorEmail(string email);
        Task Alterar(Usuario usuario);
        Task Remover(Usuario usuario);
        Task<Usuario?> BuscarPorId(int id);
    }
}