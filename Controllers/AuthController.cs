using Microsoft.AspNetCore.Mvc;
using UsersManager.Models;
using UsersManager.Services.AuthService;

namespace UsersManager.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthInterface _authInterface;
    public AuthController(IAuthInterface authInterface)
    {
        _authInterface = authInterface;
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(UserDTOlogin usuarioLogin)
    {
        var response = await _authInterface.Login(usuarioLogin); 
        return Ok(response);
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Register(UserCreateDTO userRegister)
    {
        var response = await _authInterface.Register(userRegister);
        return Ok(response);
    }
    
    
}