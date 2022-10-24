namespace PruebaAPI.Models.Request
{
    public class PersonaRequest
    {
        public int IdPersona { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }
        public string? Correo { get; set; }
        public string? Direccion { get; set; }
        public string? Estado { get; set; }
    }
}
