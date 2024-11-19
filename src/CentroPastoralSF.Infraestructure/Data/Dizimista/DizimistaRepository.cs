using CentroPastoralSF.Domain.Dizimista;
using Microsoft.EntityFrameworkCore;
using DizimistaDomain = CentroPastoralSF.Domain.Dizimista.Dizimista;

namespace CentroPastoralSF.Infraestructure.Data.Dizimista
{
    public class DizimistaRepository(CentroPastoralSFContext context) : IDizimistaRepository
    {
        public async Task<DizimistaDomain> Adicionar(DizimistaDomain dizimista)
        {
            await context.AddAsync(dizimista);
            await context.SaveChangesAsync();

            return dizimista;
        }

        public async Task Atualizar(DizimistaDomain dizimista)
        {
            context.Update(dizimista);
            await context.SaveChangesAsync();
        }

        public async Task<DizimistaDomain?> BuscarPorId(int id)
        {
            return await context.Dizimistas
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task<IQueryable<DizimistaDomain?>> BuscarTodos()
        {
            return await Task.Run(() => context.Dizimistas
                .AsNoTracking()
                .OrderBy(d => d.Nome.Nome)
                .ThenBy(d => d.Id));
        }

        public async Task<int> BuscarTotal()
        {
            return await context.Dizimistas.AsNoTracking().CountAsync();
        }

        public async Task Remover(DizimistaDomain dizimista)
        {
            context.Remove(dizimista);
            await context.SaveChangesAsync();
        }
    }
}