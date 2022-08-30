using AutoMapper;
using back_end.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PeliculasAPI.Controllers;
using Microsoft.EntityFrameworkCore;
using back_end;
using back_end.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Consul;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace back_end.Controllers

{
    [Route("api/productos")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme ,Roles ="admin")]

    public class ProductosController :  Controller
    {
        private readonly ILogger<GenerosController> logger;
        private readonly DataContext context;
        private readonly IMapper mapper;
        

        public ProductosController(ILogger<GenerosController> logger,
            DataContext context,
            IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        // POST: ProductosController/Create
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Post([FromBody] ProductoCreacionDTO ProductosCreacionDTO)
        {
           
            Console.Write("<<<<>><<<<<<");
            
            var Producto = mapper.Map<productos>(ProductosCreacionDTO);
         
            context.productos.Add(Producto);
           
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductoDTO>>> Todos()
        {
            var productosl = await context.productos.ToListAsync();
            return mapper.Map<List<ProductoDTO>>(productosl);
        }

        [HttpGet("todosprotalla")]
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductotallaDTO>>> Todospt()
        {
            var productosl = await context.productos
                .Include(f => f.marca)
                .Include(f => f.genero)
                 .Include(k => k.productocolor)
                   .ThenInclude(m => m.color)
                .Include(f => f.productotalla)
                  .ThenInclude(m => m.talla)
               
                 
               .ToListAsync();
            return mapper.Map<List<ProductotallaDTO>>(productosl);
        }



        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductoDTO>> Get(int Id)
        {
            var producto = await context.productos.FirstOrDefaultAsync(x => x.Id == Id);

            if (producto == null)
            {
                return NotFound();
            }

            return mapper.Map<ProductoDTO>(producto);
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int Id, [FromBody] ProductoCreacionDTO ProductosCreacionDTO)
        {
            var producto = await context.productos.FirstOrDefaultAsync(x => x.Id == Id);

            if (producto == null)
            {
                return NotFound();
            }
           
          
            producto = mapper.Map(ProductosCreacionDTO, producto);

            await context.SaveChangesAsync();
            return NoContent();
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.productos.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new productos() { Id = id });
            await context.SaveChangesAsync();
            return NoContent();
        }

    }

}
