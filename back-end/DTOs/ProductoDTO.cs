using back_end.Entidades;
using PeliculasAPI.DTOs;
using System.ComponentModel.DataAnnotations;

namespace back_end.DTOs
{
    public class ProductoDTO
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido:GERCEL")]
        public string? Nombre { get; set; }
        public string? Precio { get; set; }
        public string? Foto { get; set; }
     
    }
}
