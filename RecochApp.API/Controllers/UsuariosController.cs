using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecochApp.Infrastructure.Data;
using RecochApp.Core.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace RecochApp.API.Controllers
{

    // Controlador para gestionar las operaciones relacionadas con los usuarios, incluyendo registro, inicio de sesión y acceso al perfil del usuario. Este controlador utiliza JWT para la autenticación y autorización de los usuarios.
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UsuariosController(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        //PERFIL
        // Este método permite a los usuarios autenticados acceder a su perfil. Utiliza el atributo [Authorize] para asegurar que solo los usuarios autenticados puedan acceder a esta ruta. El método extrae el nombre y el correo electrónico del usuario a partir de los claims presentes en el token JWT y devuelve esta información en la respuesta.
        [HttpGet("perfil")]
        [Authorize]
        public IActionResult Perfil()
        {
            var nombre = User.Identity?.Name;
            var correo = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            return Ok(new
            {
                mensaje = "Accediste correctamente 🔐",
                nombre = nombre,
                correo = correo
            });
        }

        // REGISTRO
        // Este método permite a los nuevos usuarios registrarse en la aplicación. Recibe un objeto Usuario en el cuerpo de la solicitud, lo agrega a la base de datos y guarda los cambios. Finalmente, devuelve el usuario registrado en la respuesta.
        [HttpPost("registro")]
        public async Task<IActionResult> Registro(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return Ok(usuario);
        }

                // LOGIN
        // Este método permite a los usuarios existentes iniciar sesión en la aplicación. Recibe un objeto Usuario en el cuerpo de la solicitud, verifica las credenciales y, si son correctas, genera un token JWT que se devuelve en la respuesta.
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Usuario login)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == login.Correo);

            // Verificar si el usuario existe y si la contraseña es correcta
            if (usuario == null)
                return Unauthorized("Usuario no existe");

            // En un entorno real, deberías usar un método de hashing seguro para comparar las contraseñas, como BCrypt o Argon2. Aquí se asume que el PasswordHash ya es un hash seguro.
            if (usuario.PasswordHash != login.PasswordHash)
                return Unauthorized("Contraseña incorrecta");

            // Crear los claims para el token JWT
            var claims = new[]
            {
        new Claim(ClaimTypes.Name, usuario.Nombre),
        new Claim(ClaimTypes.Email, usuario.Correo)
    };

            // Generar el token JWT
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // El token expira en 2 horas
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            // Devolver el token JWT en la respuesta
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }
    }
}