using CentroPastoralSF.Domain.Usuario;
using Microsoft.EntityFrameworkCore;
using UsuarioDomain = CentroPastoralSF.Domain.Usuario.Usuario;

namespace CentroPastoralSF.Infraestructure.Data.Usuario
{
    public class UsuarioRepository(CentroPastoralSFContext context) : IUsuarioRepository
    {
        public async Task<UsuarioDomain> Adicionar(UsuarioDomain usuario)
        {
            await context.AddAsync(usuario);
            await context.SaveChangesAsync();

            return usuario;
        }

        public async Task Atualizar(UsuarioDomain usuario)
        {
            context.Update(usuario);

            await context.SaveChangesAsync();
        }

        public async Task<UsuarioDomain?> BuscarPorEmail(string email)
        {
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Login.Endereco == email);
        }

        public async Task<UsuarioDomain?> BuscarPorId(int id)
        {
            return await context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IQueryable<UsuarioDomain?>> BuscarTodos()
        {
            return await Task.Run(() => context.Usuarios
                .OrderBy(u => u.Nome.Nome)
                .ThenBy(u => u.Id)
                .AsQueryable());
        }

        public async Task Remover(UsuarioDomain usuario)
        {
            context.Remove(usuario);

            await context.SaveChangesAsync();
        }
    }
}