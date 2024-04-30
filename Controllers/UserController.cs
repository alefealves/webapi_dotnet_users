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

    [HttpGet]
    public async Task<ActionResult<ResponseModel<List<UserModel>>>> GetUsers()
    {
        var users = await _userInterface.GetUsers();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseModel<UserModel>>> GetUser(int id)
    {
        var user = await _userInterface.GetUser(id);
        return Ok(user);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseModel<UserModel>>> CreateUser([FromBody] UserDTO newUser)
    {
        var user = await _userInterface.CreateUser(newUser);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseModel<UserModel>>> UpdateUser(int id, [FromBody] UserDTO updateUser)
    {
        var user = await _userInterface.UpdateUser(id, updateUser);
        return Ok(user);
    }
    
    [HttpDelete("{id}")] 
    public async Task<ActionResult<ResponseModel<UserModel>>> DeleteUser(int id)
    {
        var user = await _userInterface.DeleteUser(id);
        return Ok(user);
    }
}
    