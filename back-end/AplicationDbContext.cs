using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Entidades;
using back_end.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

namespace back_end
{


    public class DataContext : IdentityDbContext
    {


        public DataContext(DbContextOptions<DataContext> options) : base(options)
       {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<productocolor>() .HasKey(x=> new { x.productosId,x.ColorId });
            builder.Entity<productotalla>().HasKey(x => new { x.productosId, x.tallaId });

            // builder.Entity<productocolor>().HasKey(x => new { x.productotallacolorId, x.colorId });

            base.OnModelCreating(builder);
        }
        public DbSet<Comentario> Comentario { get; set; }
        public DbSet<productos> productos { get; set; }
       
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Rating> Ratings { get; set; }
       
       
        public DbSet<productocolor> productotallacolor { get; set; }
        
        public DbSet<talla> talla { get; set; }
        public DbSet<Marca> marca { get; set; }
        public DbSet<Color> color { get; set; }



    }

   
}