using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class CredencialesUsuario
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public string Password2 { get; set; }

        public string ayudapass { get; set; }
        public string sexo { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
     



    }
}
 