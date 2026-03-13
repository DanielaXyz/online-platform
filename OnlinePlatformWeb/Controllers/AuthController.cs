using Microsoft.AspNetCore.Mvc;
using OnlinePlatformWeb.DataTransferObjects;
using OnlinePlatformWeb.Services.Interfaces;

namespace ShoppingCartService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(ITokenService _tokenService) : ControllerBase
{
    [HttpPost("token")]
    public IActionResult GetToken([FromBody] TokenRequest request)
    {
        //TODO: authenticate user credentials
        var token = _tokenService.GenerateToken(request.UserId, request.Username);
        return Ok(new { token });
    }
}