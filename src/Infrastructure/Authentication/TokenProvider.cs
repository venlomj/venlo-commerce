﻿using System.Security.Claims;
using System.Text;
using Application.Abstractions.Authentication;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authentication
{
    public class TokenProvider(IConfiguration configuration) : ITokenProvider
    {
        public string Create(User user)
        {
            var secretKey = configuration["Jwt:Secret"]!;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var expirationMinutes = Convert.ToInt32(configuration["Jwt:ExpirationInMinutes"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                    [
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email)
                    ]),
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
