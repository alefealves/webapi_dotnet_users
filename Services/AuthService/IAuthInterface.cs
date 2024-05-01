using Microsoft.AspNetCore.Identity;
using UsersManager.Models;

namespace UsersManager.Services.AuthService;

public interface IAuthInterface
{
    Task<ResponseModel<UserCreateDTO>> Register(UserCreateDTO userCreateRegister);
    Task<ResponseModel<string>> Login(UserDTOlogin userLogin);
}