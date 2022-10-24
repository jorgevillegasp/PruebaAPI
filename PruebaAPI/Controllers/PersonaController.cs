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
    public class PersonaController : ControllerBase
    {
        private readonly PersonaInterface _personaInterface;

        public PersonaController(PersonaInterface personaInterface)
        {
            _personaInterface = personaInterface;
        }


        // GET: api/<PersonaController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            Response _resp = new Response();
            try
            {
                _resp.Data = await _personaInterface.GetAll();
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }
        }

        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            Response _resp = new Response();
            try
            {
                _resp.Data = await _personaInterface.GetById(id);
    
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
        public async Task<IActionResult> Add(PersonaRequest request)
        {

            Response _resp = new Response();

            try
            {
                limpiarEspacios(request);

                if (request.Nombres == "" || request.Apellidos == "" || request.Correo == "" || request.Direccion == "")
                {
                    _resp.Message = "Los campos no deben de estar vacios";
                    return Ok(_resp);
                }


                if (request.Estado != "ON" || request.Estado != "OF" || request.Estado != "")
                {
                    //El estado que sera por defecto
                    request.Estado = "OF";
                }

                _resp.Complete = await _personaInterface.Add(request);
                _resp.Message = "La persona se guardo con exito";

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
        [Route("edit")]
        public async Task<IActionResult> Edit(PersonaRequest request)
        {
            Response _resp = new Response();

            try
            {
                limpiarEspacios(request);

                //Consultamos si la persona existe de no ser asi saltara una acepcion
                Persona? per = await _personaInterface.GetById(request.IdPersona);

                if (request.Nombres == "")      request.Nombres = per.Nombres;
                if (request.Apellidos == "")    request.Apellidos = per.Apellidos;
                if (request.Correo == "")       request.Correo = per.Correo;
                if (request.Direccion == "")    request.Direccion = per.Direccion;
                if (request.Estado == "")       request.Estado = per.Estado;

                _resp.Data = await _personaInterface.Edit(request);
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
                _resp.Complete = await _personaInterface.Delete(id);
                _resp.Message = "Se elimino con exito";
                return Ok(_resp);
            }
            catch (Exception ex)
            {
                _resp.Message = ex.Message;
                return Ok(_resp);
            }
        }

        private PersonaRequest limpiarEspacios(PersonaRequest request)
        {
            request.Nombres    = request.Nombres?.Trim();
            request.Apellidos  = request.Apellidos?.Trim();
            request.Correo     = request.Correo?.Trim();
            request.Direccion  = request.Direccion?.Trim();
            request.Estado     = request.Estado?.Trim();

            return request;
        }

    }
}
