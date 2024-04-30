using UsersManager.Models;

namespace UsersManager.Services;

public interface IUserInterface
{
    Task<ResponseModel<List<UserModel>>> GetUsers();
    Task<ResponseModel<UserModel>> GetUser(int id);
    Task<ResponseModel<UserModel>> CreateUser(UserDTO newUser);
    Task<ResponseModel<UserModel>> UpdateUser(int id, UserDTO updateUser);
    Task<ResponseModel<UserModel>> DeleteUser(int id);
}