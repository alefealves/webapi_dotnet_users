using UsersManager.Models;

namespace UsersManager.Services;

public interface IUserInterface
{
    Task<ResponseModel<List<UserModel>>> GetUsers();
    Task<ResponseModel<UserModel>> GetUser(int id);
    Task<ResponseModel<UserModel>> CreateUser(UserCreateDTO newUserCreate);
    Task<ResponseModel<UserModel>> UpdateUser(UserUpdateDTO updateUser);
    Task<ResponseModel<UserModel>> DeleteUser(UserDeleteDTO userDeleteDto);
}