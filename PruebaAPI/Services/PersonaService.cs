using PruebaAPI.Interfaces;
using PruebaAPI.Models;
using PruebaAPI.Models.Request;

namespace PruebaAPI.Services
{
    public class PersonaService : PersonaInterface
    
    {
        private readonly pruebaApiContext _context;

        public PersonaService(pruebaApiContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(PersonaRequest request)
        {
            try
            {
                Persona _persona = new Persona();

                _persona.Nombres    = request.Nombres;
                _persona.Apellidos  = request.Apellidos;
                _persona.Correo     = request.Correo;
                _persona.Direccion  = request.Direccion;
                _persona.Estado     = request.Estado;

                _context.Add(_persona);
                _context.SaveChanges();

                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var _articulo = _context.Personas.First(a => a.IdPersona == id);
                if (_articulo != null)
                {
                    _context.Personas.Remove(_articulo);
                    _context.SaveChanges();
                    return await Task.FromResult(true);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> Edit(PersonaRequest request)
        {
            try
            {
                Persona? _persona =  _context.Personas.Find(request.IdPersona);

                if (_persona == null)
                    return await Task.FromResult(false);

                
                _persona.IdPersona  = request.IdPersona;
                _persona.Nombres    = request.Nombres?.Trim();
                _persona.Apellidos  = request.Apellidos?.Trim();
                _persona.Correo     = request.Correo?.Trim();
                _persona.Direccion  = request.Direccion?.Trim();
                _persona.Estado     = request.Estado?.Trim();

                _context.Update(_persona);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }

            return await Task.FromResult(true);
        }

        public async Task<List<Persona>> GetAll()
        {
            return await Task.FromResult(_context.Personas.OrderByDescending(c => c.IdPersona).ToList());
        }

        public async Task<Persona> GetById(int id)
        {
            return await Task.FromResult(_context.Personas.First(a => a.IdPersona == id));
        }
    }
}
