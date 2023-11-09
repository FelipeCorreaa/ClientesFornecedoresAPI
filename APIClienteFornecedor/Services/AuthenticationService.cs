using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.IO; // Adicione o namespace para trabalhar com arquivos

namespace APIClienteFornecedor.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = "Host=localhost;Port=5432;Pooling=true;Database=ClientesFornecedoresAPI;User Id=postgres;Password=1234";
        }

        public string GenerateToken(string accessKeyId, string accessKeySecret)
        {
            if (accessKeyId != "Acces1234" || accessKeySecret != "Acces1234")
            {
                throw new UnauthorizedAccessException("Credenciais inválidas.");
            }

            string generatedToken = GenerateJwtToken();

            // Escreve o token no arquivo token.txt
            File.WriteAllText("token.txt", generatedToken);

            return generatedToken;
        }

        private string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, "Acces1234"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string ReadTokenFromFile()
        {
            try
            {
                // Lê o conteúdo do arquivo token.txt
                string token = File.ReadAllText("token.txt");
                return token;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Erro ao ler o token do arquivo: " + ex.Message);
                return null;
            }
        }
    }
}
