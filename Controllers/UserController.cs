using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UsersManager.Models;
using UsersManager.Services;

namespace UsersManager.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserInterface _userInterface;

    public UserController(IUserInterface userInterface)
    {
        _userInterface = userInterface;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetUsers()
    {
        var users = await _userInterface.GetUsers();
        return Ok(users);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<UserModel>>> GetUser(int id)
    {
        var user = await _userInterface.GetUser(id);
        return Ok(user);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<ResponseModel<UserModel>>> CreateUser([FromBody] UserCreateDTO newUserCreate)
    {
        var user = await _userInterface.CreateUser(newUserCreate);
        return Ok(user);
    }
    
    [Authorize]
    [HttpPut]
    public async Task<ActionResult<ResponseModel<UserModel>>> UpdateUser([FromBody] UserUpdateDTO updateUser)
    {
        var user = await _userInterface.UpdateUser(updateUser);
        return Ok(user);
    }
    
    [Authorize]
    [HttpDelete] 
    public async Task<ActionResult<ResponseModel<UserModel>>> DeleteUser([FromBody] UserDeleteDTO userDeleteDto)
    {
        var user = await _userInterface.DeleteUser(userDeleteDto);
        return Ok(user);
    }
}
    