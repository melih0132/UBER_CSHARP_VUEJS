using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UberApi.Models;
using UberApi.Models.EntityFramework;

namespace UberApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly S221UberContext _context;

        public LoginController(IConfiguration config, S221UberContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<UserResponse>> Login([FromBody] UserLogin userLogin)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.EmailUser == userLogin.Email);
            if (client != null && BCrypt.Net.BCrypt.Verify(userLogin.Password, client.MotDePasseUser))
            {
                return GenerateUserResponse(
                    user: client,
                    role: "Client",
                    userId: client.IdClient
                );
            }

            var coursier = await _context.Coursiers.FirstOrDefaultAsync(c => c.EmailUser == userLogin.Email);
            if (coursier != null && BCrypt.Net.BCrypt.Verify(userLogin.Password, coursier.MotDePasseUser))
            {
                return GenerateUserResponse(
                    user: coursier,
                    role: "Coursier",
                    userId: coursier.IdCoursier
                );
            }

            // 3. Vérification pour les Admins (si vous avez une table Admin)
            // ... (même pattern)

            return Unauthorized("Identifiants incorrects");
        }

        private ActionResult<UserResponse> GenerateUserResponse(dynamic user, string role, int userId)
        {
            var token = GenerateJwtToken(
                email: user.EmailUser,
                role: role,
                userId: userId.ToString(),
                fullName: $"{user.PrenomUser} {user.NomUser}"
            );

            return new UserResponse
            {
                Token = token,
                Role = role,
                UserId = userId,
                Email = user.EmailUser
            };
        }

        private string GenerateJwtToken(string email, string role, string userId, string fullName)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim("fullName", fullName),
                new Claim("role", role),
                new Claim("userId", userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}