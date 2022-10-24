using System;
using System.Collections.Generic;

namespace PruebaAPI.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdPersona { get; set; }
        public string? Usuario1 { get; set; }
        public string? Clave { get; set; }
        public string? Estado { get; set; }

        public virtual Persona? IdPersonaNavigation { get; set; }
    }
}
