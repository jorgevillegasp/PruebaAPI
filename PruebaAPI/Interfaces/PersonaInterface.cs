using PruebaAPI.Models.Request;
using PruebaAPI.Models;

namespace PruebaAPI.Interfaces
{
    public interface PersonaInterface
    {
        public Task<List<Persona>> GetAll();
        public Task<Persona> GetById(int id);
        public Task<bool> Add(PersonaRequest request);
        public Task<bool> Edit(PersonaRequest request);
        public Task<bool> Delete(int id);
    }
}
