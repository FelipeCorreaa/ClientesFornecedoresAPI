using APIClienteFornecedor.Services.Tokren;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APIClienteFornecedor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public TokenController(IConfiguration configuration, ITokenService tokenService)
        {
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost]
        public IActionResult GenerateToken([FromBody] TokenRequestModel request)
        {
            if (request.AccessKeyId != "Acces1234" || request.AccessKeySecret != "Acces1234")
            {
                return Unauthorized();
            }

            string token = _tokenService.GenerateToken(request.AccessKeyId, request.AccessKeySecret);

          

            return Ok(new { Token = token });
        }
    }

    public class TokenRequestModel
    {
        public string AccessKeyId { get; set; }
        public string AccessKeySecret { get; set; }
    }
}
