using System.ComponentModel.DataAnnotations;

namespace back_end.Entidades
{
    public class productos
    {
        public int Id { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        public string? Precio { get; set; }
        public int cantidadTotal { get; set; }
        public string? Foto { get; set; }
        public int MarcaId { get; set; }
        public int GeneroId { get; set; }
        public IEnumerable<Marca> marca { get; set; }
        public IEnumerable<Genero> genero { get; set; }
        
        public IEnumerable<productocolor> productocolor { get; set; }
        public IEnumerable<productotalla> productotalla { get; set; }







    }
}
