using CentroPastoralSF.Domain.ValueObjects;

namespace CentroPastoralSF.Domain.Usuario
{
    public interface IUsuarioService
    {
        Task<Usuario> Adicionar(Usuario usuario);
        Task<IQueryable<Usuario?>> BuscarTodos();
        Task<Usuario?> BuscarPorEmail(string email);
        Task Alterar(Usuario usuario, Nomeacao nome, Email login, string senha);
        Task Remover(Usuario usuario);
        Task<Usuario?> BuscarPorId(int id);
        Usuario ValidarLogin(Usuario usuario, string email, string senha);
    }
}