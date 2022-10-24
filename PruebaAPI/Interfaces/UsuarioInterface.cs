using PruebaAPI.Models;
using PruebaAPI.Models.Request;

namespace PruebaAPI.Interfaces
{
    public interface UsuarioInterface
    {
        public Task<List<Usuario>> GetAll();
        public Task<Usuario> GetById(int id);
        public Task<bool> Add(UsuarioRequest request);
        public Task<bool> Edit(UsuarioRequest request);
        public Task<bool> Delete(int id);
    }
}
