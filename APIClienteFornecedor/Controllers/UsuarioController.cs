using APIClienteFornecedor.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System;
using System.Data;
using System.Text;

namespace APIClienteFornecedor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly string _connectionString = "Host=localhost;Port=5432;Pooling=true;Database=ClientesFornecedoresAPI;User Id=postgres;Password=1234";

        [HttpPost]
        [Authorize] // Adiciona a autorização
        public IActionResult PostUsuario(Usuario usuario)
        {
            // Se chegou aqui, o token foi validado com sucesso
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                using (var command = new NpgsqlCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "INSERT INTO Usuario (UserName, Email) VALUES (@UserName, @Email)";
                    command.Parameters.AddWithValue("@UserName", usuario.UserName);
                    command.Parameters.AddWithValue("@Email", usuario.Email);

                    try
                    {
                        command.ExecuteNonQuery();
                        return Ok();
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
            }
        }
    }
}