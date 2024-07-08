namespace CentroPastoralSF.Domain.Dizimista
{
    public class DizimistaService(IDizimistaRepository dizimistaRepository) : IDizimistaService
    {
        public async Task Alterar(Dizimista dizimista)
        {
            await dizimistaRepository.Atualizar(dizimista);
        }

        public Task<Dizimista?> BuscarPorId(int id)
        {
            return dizimistaRepository.BuscarPorId(id);
        }

        public async Task<IQueryable<Dizimista?>> BuscarTodos()
        {
            return await dizimistaRepository.BuscarTodos();
        }

        public async Task<Dizimista> Incluir(Dizimista dizimista)
        {
            //Todo: Se tiver outras regras de negócio, validar aqui!

            return await dizimistaRepository.Adicionar(dizimista);
        }

        public async Task Remover(Dizimista dizimista)
        {
            await dizimistaRepository.Remover(dizimista);
        }
    }
}