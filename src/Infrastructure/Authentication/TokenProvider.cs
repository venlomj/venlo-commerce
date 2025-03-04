using System.Security.Claims;
using System.Text;
using Application.Abstractions.Authentication;
using Domain.Entities;
using Infrastructure.Persistence.SQL;
using iText.Commons.Actions.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication
{
    public class TokenProvider(IConfiguration configuration,
        VenloCommerceDbContext context) : ITokenProvider
    {
        public async Task<string> Create(User user)
        {
            var secretKey = configuration["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Fetch user roles from the database
            var roleNames = await context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.Role.Name)
                .ToListAsync();

            var expirationMinutes = Convert.ToInt32(configuration["Jwt:ExpirationInMinutes"]);

            // Claims should include roles and other necessary information
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            claims.AddRange(roleNames.Select(r => new Claim(ClaimTypes.Role, r)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                SigningCredentials = credentials,
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"]
            };

            var handler = new JsonWebTokenHandler();

            var token = handler.CreateToken(tokenDescriptor);

            return token;
        }
    }
}
