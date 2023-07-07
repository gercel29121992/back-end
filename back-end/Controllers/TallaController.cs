using AutoMapper;
using back_end.DTOs;
using back_end.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasAPI.DTOs;

namespace back_end.Controllers
{
    [Route("api/talla")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class TallaController : Controller
    {
        private readonly ILogger<TallaController> logger;
        private readonly DataContext context;
        private readonly IMapper mapper;

        public TallaController(
           ILogger<TallaController> logger,
           DataContext context,
           IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }




        [HttpPost]

        public async Task<ActionResult> Post([FromBody] TallaCreacionDTO tallaCreacionDTO)
        {
     
            var talla = mapper.Map<talla>(tallaCreacionDTO);
            context.Add(talla);

            await context.SaveChangesAsync();

            return NoContent();
        }


        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<TallaDTO>>> Todos()
        {
            Console.Write("entro");
            var tallas = await context.talla.ToListAsync();
            return mapper.Map<List<TallaDTO>>(tallas);
        }

        [HttpGet("{Id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<TallaDTO>> Get(int Id)
        {
            var talla = await context.talla.FirstOrDefaultAsync(x => x.Id == Id);
            Console.Write("entro");
            if (talla == null)
            {
                return NotFound();
            }

            return mapper.Map<TallaDTO>(talla);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] TallaCreacionDTO tallaCreacionDTO)
        {
            var talla = await context.talla.FirstOrDefaultAsync(x => x.Id == Id);

            if (talla == null)
            {
                return NotFound();
            }


            talla = mapper.Map(tallaCreacionDTO, talla);

            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id:int}")]
        
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.talla.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }
            Console.Write("gerceñ");
            context.Remove(new talla() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }




    }
}
