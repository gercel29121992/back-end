namespace back_end.Entidades
{
    public class talla
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<productotalla> productotalla { get; set; }
    }
}
