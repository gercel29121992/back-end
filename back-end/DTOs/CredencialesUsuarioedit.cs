using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasAPI.DTOs
{
    public class CredencialesUsuarioEdit
    {
        public string id { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        
      
        public string sexo { get; set; }

        public string nombre { get; set; }
        public string apellido { get; set; }
     



    }
}
 