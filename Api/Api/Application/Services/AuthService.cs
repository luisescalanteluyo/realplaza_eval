using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IConfiguration _config;
        public AuthService(IUserRepository users, IConfiguration config)
        {
            _users = users;
            _config = config;
        }

        public async Task<Guid> RegisterAsync(string username, string password)
        {
            var existing = await _users.GetByUsernameAsync(username);
            if (existing != null) throw new InvalidOperationException("User already exists");
            //var hash = BCrypt.Net.BCrypt.HashPassword(password);
            var user = new User { Username = username, Password = password };
            return await _users.CreateAsync(user);
        }

        public async Task<AuthResult?> LoginAsync(string username, string password)
        {
            var user = await _users.GetByUsernameAsync(username);
            if (user == null) return null;
            if (password != user.Password) return null;

            // Build JWT
            //var key = _config["Jwt:Key"]!;
            var issuer = _config["Jwt:Issuer"];
            var expires = DateTime.UtcNow.AddHours(4);

        //    var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //    new Claim(ClaimTypes.Name, user.Username),
        //    new Claim(ClaimTypes.Role, user.Role)
        //};
            var claims = new[]
           {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            //var creds = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("123456890000000000asdfghjklñzxcvbnm"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //var token = new JwtSecurityToken(
            //    issuer: issuer,
            //    claims: claims,
            //    expires: expires,
            //    signingCredentials: creds
            //);
            var token = new JwtSecurityToken(
               issuer: "IssuerDomain",
               audience: "IssuerDomain",
               claims: claims,
               expires: DateTime.Now.AddMinutes(30),
               signingCredentials: creds);

            //return new JwtSecurityTokenHandler().WriteToken(token);
            return new AuthResult { Token = new JwtSecurityTokenHandler().WriteToken(token), Expiration = expires };
        }
    }

}
