namespace CentroPastoralSF.Domain.Dizimista
{
    public interface IDizimistaService
    {
        Task<Dizimista> Incluir(Dizimista dizimista);
        Task Alterar(Dizimista dizimista);
        Task<IQueryable<Dizimista?>> BuscarTodos();
        Task<Dizimista?> BuscarPorId(int id);
        Task Remover(Dizimista dizimista);
    }
}