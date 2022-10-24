namespace PruebaAPI.Models.Request
{
    public class UsuarioRequest
    {
        public int IdUsuario { get; set; }
        public int? IdPersona { get; set; }
        public string? Usuario1 { get; set; }
        public string? Clave { get; set; }
        public string? Estado { get; set; }
    }
}
