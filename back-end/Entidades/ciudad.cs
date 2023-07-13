namespace back_end.Entidades
{
    public class ciudad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public IEnumerable<userapp> userapp { get; set; }
    }
}
