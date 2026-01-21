using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiPedido.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 🔐 Validación simple (en producción, validar contra base de datos)
            if (request.Username == "admin" && request.Password == "12345")
            {
                // Generar el token
                var token = GenerateJwtToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
        }

        private string GenerateJwtToken(string username)
        {
            var jwtKey = "MiClaveSecretaSuperSegura12345678901234567890"; // Misma clave del Program.cs
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Claims (información del usuario en el token)
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            // Crear el token
            var token = new JwtSecurityToken(
                issuer: "ApiPedidoIssuer",
                audience: "ApiPedidoAudience",
                claims: claims,
                expires: DateTime.Now.AddHours(1), // El token expira en 1 hora
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Modelo para recibir el login
    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
