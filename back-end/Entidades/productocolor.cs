namespace back_end.Entidades
{
    public class productocolor
    {
      

        public int productosId { get; set; }
        public int ColorId { get; set; }

 public int cantidad { get; set; }
        public Color color { get; set; }
       
        public productos productos { get; set; }
    }
}
