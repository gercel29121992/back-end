using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class UsuarioDTO
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string ayudapass { get; set; }
        public string sexo { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
        public string Cargo { get; set; }
        public int? Estado { get; set; }


    }
}
