using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NPost.Modules.Users.Services;
using NPost.Modules.Users.Shared.DTO;

namespace NPost.Modules.Users.Infrastructure.Services
{
    internal class JwtProvider : IJwtProvider
    {
        private readonly IOptions<JwtOptions> _options;
        private readonly SigningCredentials _signingCredentials;

        public JwtProvider(IOptions<JwtOptions> options)
        {
            _options = options;
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Value.SecretKey));
            _signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256);
        }

        public JwtDto CreateToken(string userId, string role)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("User id claim can not be empty.", nameof(userId));
            }

            var now = DateTime.UtcNow;
            var nowEpoch = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            var jwtClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, userId),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, nowEpoch.ToString()),
                new Claim(ClaimTypes.Role, role)
            };

            var expires = now.AddMinutes(_options.Value.ExpiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _options.Value.Issuer,
                claims: jwtClaims,
                notBefore: now,
                expires: expires,
                signingCredentials: _signingCredentials
            );
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                AccessToken = token,
                Role = role ?? string.Empty,
            };
        }
    }
}