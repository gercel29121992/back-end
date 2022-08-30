namespace back_end.Entidades
{
    public class Color
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<productocolor> productocolor { get; set; }
    }
}

