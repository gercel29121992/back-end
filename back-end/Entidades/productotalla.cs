namespace back_end.Entidades
{
    public class productotalla
    {
       

        public int productosId { get; set; }
      
        public int tallaId { get; set; }

        public int cantidad { get; set; }
        
        public talla talla { get; set; }
        public productos productos { get; set; }
    }
}
