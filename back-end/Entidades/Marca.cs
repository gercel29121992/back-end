namespace back_end.Entidades
{
    public class Marca
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public List<productos> productos { get; set; }
    }
}
