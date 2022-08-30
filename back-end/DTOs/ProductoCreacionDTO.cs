

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Utilidades;

namespace back_end.DTOs
{
    public class ProductoCreacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido:GERCELDTO")]
        public string? Nombre { get; set; }
        public string? Precio { get; set; }
        public string? Foto { get; set; }
        public int marcaId { get; set; }
        public int cantidadtotal { get; set; }
        public int generoId { get; set; }

        [ModelBinder(BinderType =typeof(TypeBinder<List<colortallacreacionlistadoDTO>>))]
        public List<colortallacreacionlistadoDTO> colortallacreacionlistadoDTO { get; set; }
    }
}
