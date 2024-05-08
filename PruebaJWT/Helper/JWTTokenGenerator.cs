using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PruebaJWT.Helper
{
    public class JWTTokenGenerator
    {

        public string GetToken(string nombre)
        {

            List<Claim> claims = new();
            claims.Add(new Claim("Rol", "Admin"));
            claims.Add(new Claim(ClaimTypes.Name, nombre));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iss, "Saludos"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Aud, "Prueba"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, DateTime.Now.AddMinutes(5).ToString()));


            JwtSecurityTokenHandler handler = new();
            var token = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = "Saludos",
                Audience = "Prueba",
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(-1),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("ESTAESMILLAVEDECIFRADODELTOKEN2024")), SecurityAlgorithms.HmacSha256)
            };

            return handler.CreateEncodedJwt(token);

        }
    }
}
