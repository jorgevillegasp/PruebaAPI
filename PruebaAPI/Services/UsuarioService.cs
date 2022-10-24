using Microsoft.EntityFrameworkCore;
using PruebaAPI.Interfaces;
using PruebaAPI.Models;
using PruebaAPI.Models.Request;

namespace PruebaAPI.Services
{
    public class UsuarioService : UsuarioInterface
    {
        private readonly pruebaApiContext _context;

        public UsuarioService(pruebaApiContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(UsuarioRequest request)
        {
            try
            {
                Usuario _usuario = new Usuario();

                _usuario.IdPersona  = request.IdPersona;
                _usuario.Usuario1   = request.Usuario1;
                _usuario.Clave      = request.Clave;
                _usuario.Estado     = request.Estado;

                _context.Add(_usuario);
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
                var _usuario = _context.Usuarios.First(a => a.IdUsuario == id);
                if (_usuario != null)
                {
                    _context.Usuarios.Remove(_usuario);
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

        public async Task<bool> Edit(UsuarioRequest request)
        {
            try
            {
                Usuario? _usuario = _context.Usuarios.Find(request.IdUsuario);

                if (_usuario == null)
                    return await Task.FromResult(false);

                _usuario.IdPersona  = request.IdPersona;
                _usuario.Usuario1   = request.Usuario1?.Trim();
                _usuario.Clave      = request.Clave?.Trim();
                _usuario.Estado     = request.Estado?.Trim();

                _context.Update(_usuario);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }

            return await Task.FromResult(true);
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _context.Usuarios.OrderByDescending(c => c.IdUsuario).ToListAsync();
        }

        public async Task<Usuario> GetById(int id)
        {
            return await Task.FromResult(_context.Usuarios.First(a => a.IdUsuario == id));
        }
    }
}
