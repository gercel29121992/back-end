using Microsoft.AspNetCore.Identity;

namespace back_end.Entidades
{
    public class userapp:IdentityUser
    {
        public string ayudapass { get; set; }
        public string Sexo { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cargo { get; set; }
        public int? Estado { get; set; }
        public int ciudadId { get; set; }
        public ciudad ciudad { get; set; }




    }
}
