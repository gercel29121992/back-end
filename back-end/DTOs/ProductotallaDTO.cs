namespace back_end.DTOs
{
    public class ProductotallaDTO
    {
      
        public string? Nombre { get; set; }
        public string? Precio { get; set; }
        public string? Foto { get; set; }
        public int marcaId { get; set; }
        public int generoId { get; set; }
        public List<DetalleDTO> DetalleDTO { get; set; }
    }
}
