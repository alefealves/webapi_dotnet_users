using Microsoft.EntityFrameworkCore;
using UsersManager.Data;
using UsersManager.Enums;
using UsersManager.Models;
using UsersManager.Services.SenhaService;

namespace UsersManager.Services;

public class UserService : IUserInterface
{
    private readonly AppDbContext _context;
    private readonly ISenhaInterface _senhaInterface;

    public UserService(AppDbContext context, ISenhaInterface senhaInterface)
    {
        _context = context;
        _senhaInterface = senhaInterface;
    }
    
    public async Task<ResponseModel<List<UserModel>>> GetUsers()
    {
        ResponseModel<List<UserModel>> response = new ResponseModel<List<UserModel>>();
        try
        {

            var users = await _context.Users.OrderBy(u => u.Id).ToListAsync();

            if (users.Count == 0)
            {
                response.Data = null;
                response.Message = "Nenhum usuário foi encontrado.";
                response.Status = false;
                return response;
            }

            response.Data = users;
            response.Message = "Success";
            return response;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<UserModel>> GetUser(int id)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        try
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(user == null)
            {
                response.Message = "Usuário não encontrado.";
                return response;
            }

            response.Data = user;
            response.Message = "Success";

            return response;

        }catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<UserModel>> CreateUser(UserCreateDTO newUserCreate)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        
        try
        {
            response = await UserEmailExist(newUserCreate);
            if (!response.Status)
            {
                return response;
            }
            
            response = await userAdmin(newUserCreate.IdUserAlter);
            if (!response.Status)
            {
                return response;
            }

            _senhaInterface.CriarSenhaHash(newUserCreate.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            UserModel user = new UserModel()
            {
                Nome = newUserCreate.Nome,
                Sobrenome = newUserCreate.Sobrenome,
                Email = newUserCreate.Email,
                NivelAcesso = newUserCreate.NivelAcesso,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt,
                IdUserUpdate = newUserCreate.IdUserAlter
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            response.Data = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
            response.Message = "Success.";
            return response;
            
        }catch(Exception ex)
        {
            response.Message = ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<UserModel>> UpdateUser(UserUpdateDTO updateUser)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        
        try
        {
            UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateUser.IdUser);
            if (user == null)
            {
                response.Data = null;
                response.Message = "Usuário não encontrado.";
                response.Status = false;
                return response;
            }
            
            response = ValidateUpdateUser(updateUser);
            if (!response.Status)
            {
                return response;
            }
            
            response = await userAdmin(updateUser.IdUserAlter);
            if (!response.Status)
            {
                return response;
            }

            if (updateUser.Nome != null)
                user.Nome = updateUser.Nome;
            if (updateUser.Sobrenome != null)
                user.Sobrenome = updateUser.Sobrenome;
            if (updateUser.Email != null)
                user.Email = updateUser.Email;
            if (updateUser.Senha != null)
            {
                _senhaInterface.CriarSenhaHash(updateUser.Senha, out byte[] senhaHash, out byte[] senhaSalt);
                user.SenhaHash = senhaHash;
                user.SenhaSalt = senhaSalt;
            }

            if (updateUser.NivelAcesso != null)
                user.NivelAcesso = (Acesso)Enum.Parse(typeof(Acesso), updateUser.NivelAcesso);
            
            user.IdUserUpdate = updateUser.IdUserAlter;
            user.UpdateAt = DateTime.UtcNow;

            _context.Update(user);
            await _context.SaveChangesAsync();

            response.Data = await _context.Users.FirstOrDefaultAsync(x => x.Id == updateUser.IdUser);
            response.Message = "Success.";

            return response;
        }
        catch (Exception ex)
        {
            response.Message = ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<UserModel>> DeleteUser(UserDeleteDTO userDeleteDto)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userDeleteDto.IdUserDelete);
            if(user == null)
            {
                response.Message = "Usuário não encontrado.";
                return response;
            }

            response = await userAdminMaster(user.Id);
            if (!response.Status)
            {
                return response;
            }

            response = await userAdmin(userDeleteDto.IdUserAlter);
            if (!response.Status)
            {
                return response;
            }

            user.IdUserUpdate = userDeleteDto.IdUserAlter;
            user.UpdateAt = DateTime.UtcNow;
            user.Ativo = false;
            _context.Update(user);
            await _context.SaveChangesAsync();

            response.Data = null;
            response.Message = "Usuário Removido com sucesso!";

            return response;

        }catch(Exception ex)
        {
            response.Message = ex.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<UserModel>> userAdminMaster(int id)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if ((user.Id == 1) && (user.Nome == "admin") && (user.NivelAcesso == Acesso.ADMIN))
        {
            response.Data = null;
            response.Status = false;
            response.Message = "Usuário ADMIN não pode ser alterado!";
        }
        else
        {
            response.Data = null;
            response.Status = true;
            response.Message = "Success";
        }
        return response;
    }
    
    public async Task<ResponseModel<UserModel>> userAdmin(int id)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (user.NivelAcesso == Acesso.ADMIN)
        {
            response.Data = null;
            response.Status = true;
            response.Message = "Success";
        }
        else
        {
            response.Data = null;
            response.Status = false;
            response.Message = "Usuário sem privilégios para esta ação.";
        }
        return response;
    }
    
    public async Task<ResponseModel<UserModel>> UserEmailExist(UserCreateDTO newUserCreate)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == newUserCreate.Email);
        if (user != null)
        {
            response.Data = null;
            response.Status = false;
            response.Message = "Email já cadastrado para outro usuário!";
        }
        else
        {
            response.Data = null;
            response.Status = true;
            response.Message = "Success";
        }
        return response;
    }

    public ResponseModel<UserModel> ValidateUpdateUser(UserUpdateDTO updateUser)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();

        if ((updateUser.Nome == null) &&
            (updateUser.Sobrenome == null) &&
            (updateUser.Email == null) &&
            (updateUser.Senha == null) &&
            (updateUser.NivelAcesso == null)
           )
        {
            response.Data = null;
            response.Message = "E necessário informar o campo para a edição do usuário.";
            response.Status = false;   
        }
        else
        {
            response.Data = null;
            response.Message = "Success.";
            response.Status = true;   
        }

        return response;
    }
}