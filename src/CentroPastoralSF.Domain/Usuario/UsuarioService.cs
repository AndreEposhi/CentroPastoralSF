using CentroPastoralSF.Core.Configurations;
using CentroPastoralSF.Core.Services;

namespace CentroPastoralSF.Domain.Usuario
{
    public class UsuarioService(IUsuarioRepository usuarioRepository, CryptoService cryptoService) : IUsuarioService
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

        public async Task<Usuario> ValidarLogin(Usuario usuario, string email, string senha)
        {
            var senhaDescriptografada = await cryptoService.DecryptText(senha, ApplicationConfiguration.EncryptorKey,
                ApplicationConfiguration.EncryptorIV);

            usuario.ValidarLogin(email, senhaDescriptografada);

            return usuario;
        }
    }
}