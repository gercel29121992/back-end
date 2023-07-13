using AutoMapper;
using back_end;
using back_end.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PeliculasAPI.DTOs;
using PeliculasAPI.Utilidades;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PeliculasAPI.Controllers
{
    [Route("api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly UserManager<userapp> userManager;
        private readonly SignInManager<userapp> signInManager;
        private readonly IConfiguration configuration;
        private readonly DataContext context;
        private readonly IMapper mapper;

        public CuentasController(UserManager<userapp> userManager,
            SignInManager<userapp> signInManager,
            IConfiguration configuration,
            DataContext context,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("listadoUsuarios")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<List<UsuarioDTO>>> ListadoUsuarios([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Users.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var usuarios = await queryable.OrderBy(x => x.Email).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<UsuarioDTO>>(usuarios);
        }

        [HttpGet("Usuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<UsuarioDTO>> Usuarios([FromQuery] string email)
        {

          
            var usuario = await userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                return NoContent();
            }
            Console.Write(usuario.Nombre);

            return mapper.Map<UsuarioDTO>(usuario);
        }


        [HttpPost("Usuario")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> usuario([FromBody] string email)
        {

            Console.Write("gercel");
            Console.Write(email);

            var usuario = await userManager.FindByEmailAsync(email);
            if (usuario == null)
            {
                return NoContent();
            }
            Console.Write(usuario.Nombre);

            return NoContent();
        }

        [HttpPost("HacerAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> HacerAdmin([FromBody] string usuarioId)
        {
            var usuario = await userManager.FindByIdAsync(usuarioId);
            await userManager.AddClaimAsync(usuario, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("bloq")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> bloq([FromBody] string usuarioId)
        {
            var usuario = await userManager.FindByIdAsync(usuarioId);
            if(usuario==null)
            {
                return NoContent();
            }
            usuario.Estado = 0;
            await userManager.UpdateAsync (usuario);
            return NoContent();
        }

        [HttpPost("RemoverAdmin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult> RemoverAdmin([FromBody] string usuarioId)
        {
            var usuario = await userManager.FindByIdAsync(usuarioId);
            await userManager.RemoveClaimAsync(usuario, new Claim("role", "admin"));
            return NoContent();
        }

        [HttpPost("crear")]
        public async Task<ActionResult<RespuestaAutenticacion>> Crear([FromBody] CredencialesUsuario credenciales)
        {
            var usuario = new userapp { UserName = credenciales.Email, Email = credenciales.Email,ayudapass="holamundo",Sexo=credenciales.sexo,Nombre=credenciales.nombre,Apellido=credenciales.apellido,Estado=1,Cargo="estudiante",ciudadId=1 };
            var resultado = await userManager.CreateAsync(usuario, credenciales.Password);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest(resultado.Errors);
            }
        }


        [HttpPost("edit")]
        public async Task<ActionResult> edit([FromBody] CredencialesUsuarioEdit credenciales)
        {

            var usuario = await userManager.FindByIdAsync(credenciales.id);
            if (usuario == null)
            {
                return NoContent();
            }

            Console.Write(usuario.Nombre);
            Console.Write(credenciales.nombre);
            usuario.Nombre = credenciales.nombre;
            usuario.Apellido = credenciales.apellido;
            usuario.Sexo = credenciales.sexo;
            usuario.NormalizedEmail = credenciales.Email.ToUpper();
            usuario.NormalizedUserName = credenciales.Email.ToUpper();
            usuario.UserName = credenciales.Email.ToLower();
            usuario.Email = credenciales.Email.ToLower();

            await userManager.UpdateAsync(usuario);
          
           
           
            return Ok();
        }

        [HttpPost("login")]
        public async Task<ActionResult<RespuestaAutenticacion>> Login([FromBody] CredencialesUsuario credenciales)
        {
           
            var resultado = await signInManager.PasswordSignInAsync(credenciales.Email, credenciales.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (resultado.Succeeded)
            {
                return await ConstruirToken(credenciales);
            }
            else
            {
                return BadRequest("Login incorrecto");
            }
        }

        private async Task<RespuestaAutenticacion> ConstruirToken(CredencialesUsuario credenciales)
        {
            var claims = new List<Claim>()
            {
                
            };
            var usuario = await userManager.FindByEmailAsync(credenciales.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);
            var claimsvalues = claimsDB.FirstOrDefault();
            //si no tiene rol LE ASIGNO UM ROL ESTANDAR 
            if (claimsvalues == null)
            {
                claims = new List<Claim>()
                {
                    new Claim("email", credenciales.Email),
                    new Claim( ClaimTypes.Role, "user")
                };
            }
            else
            {
                claims = new List<Claim>()
                 {
                     new Claim("email", credenciales.Email),
                     new Claim( ClaimTypes.Role, claimsvalues.Value)
                };

               
            }
            
     
            

            
          

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:Token"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddYears(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiracion = expiracion
            };
        }
    }
}
