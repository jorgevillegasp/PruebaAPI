using Microsoft.AspNetCore.Mvc;
using PruebaAPI.Interfaces;
using PruebaAPI.Models;
using PruebaAPI.Models.Request;
using PruebaAPI.Models.Respuestas;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PruebaAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {


        private readonly UsuarioInterface _usuarioInterface;
        private readonly PersonaInterface _personaInterface;
        public UsuarioController(UsuarioInterface usuarioInterface, PersonaInterface personaInterface)
        {
            _usuarioInterface = usuarioInterface;
            _personaInterface = personaInterface;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response _resp = new Response();

            try
            {
                
                _resp.Data = await _usuarioInterface.GetAll();
                _resp.Complete = true;
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }

        }

        [HttpPost]
        [Route("agregar")]
        public async Task<IActionResult> Add(UsuarioRequest request)
        {
            Response _resp = new Response();
            try
            {

                limpiarEspacios(request);

                if (request.IdPersona == 0 || request.Usuario1 == "" || request.Clave == "" || request.IdPersona == 0)
                {
                    _resp.Message = "Los campos no deben de estar vacios";
                    return Ok(_resp);
                }

                if (request.Estado != "ON" || request.Estado != "OF" || request.Estado != "")
                {
                    //El estado que sera por defecto
                    request.Estado = "OF";
                }

                //Si no trae nada, saltara una exepcion
                Persona? per = await _personaInterface.GetById(request.IdPersona ?? 0);

                

                await _usuarioInterface.Add(request);
                _resp.Data = request;
                _resp.Complete = true;
                _resp.Message = "El usuario se guardo con exito";

                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }
        }

        /**
         * 
         * Editar un registro (Requiere de un usuario para editar)
         * 
         */

        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> Edit(UsuarioRequest request)
        {
            Response _resp = new Response();

            try
            {
                limpiarEspacios(request);

                Usuario? _usuario = await _usuarioInterface.GetById(request.IdUsuario);

                if (request.IdPersona == 0)  request.IdPersona = _usuario.IdPersona;
                if (request.Usuario1 == "")  request.Usuario1 = _usuario.Usuario1;
                if (request.Clave == "")     request.Clave = _usuario.Clave;
                if (request.Estado == "")    request.Estado = _usuario.Estado;

                _resp.Data = await _usuarioInterface.Edit(request);
                _resp.Complete = true;
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }
        }

        /**
         * 
         * Eliminar un registro de la tabla (Requiere el id)
         * 
         */

        [HttpDelete("eliminar/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            Response _resp = new Response();
            try
            {
                _resp.Complete =  await _usuarioInterface.Delete(id);
                _resp.Message = "Se elimino con exito el usuario";
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }
        }

        private UsuarioRequest limpiarEspacios(UsuarioRequest request)
        {
            request.Usuario1    = request.Usuario1?.Trim();
            request.Clave       = request.Clave?.Trim();
            request.Estado      = request.Estado?.Trim();

            return request;
        }
    }
}
