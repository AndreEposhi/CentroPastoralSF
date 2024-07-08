namespace CentroPastoralSF.Domain.Dizimista
{
    public interface IDizimistaRepository
    {
        Task<IQueryable<Dizimista?>> BuscarTodos();
        Task<Dizimista?> BuscarPorId(int id);
        Task<Dizimista> Adicionar(Dizimista dizimista);
        Task Atualizar(Dizimista dizimista);
        Task Remover(Dizimista dizimista);
    }
}