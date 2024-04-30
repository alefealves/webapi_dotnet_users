using Microsoft.EntityFrameworkCore;
using UsersManager.Data;
using UsersManager.Enums;
using UsersManager.Models;

namespace UsersManager.Services;

public class UserService : IUserInterface
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
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

    public async Task<ResponseModel<UserModel>> CreateUser(UserDTO newUser)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        
        try
        {
            response = ValidateNewUser(newUser);
            if (!response.Status)
            {
                return response;
            }
            
            Acesso nivelAcesso = (Acesso)Enum.Parse(typeof(Acesso), newUser.NivelAcesso);
            UserModel user = new UserModel(newUser.Nome, newUser.Sobrenome, newUser.Email, newUser.Senha, nivelAcesso);

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

    public async Task<ResponseModel<UserModel>> UpdateUser(int id, UserDTO updateUser)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        
        try
        {
            UserModel user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            
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

            if (updateUser.Nome != null)
                user.Nome = updateUser.Nome;
            if (updateUser.Sobrenome != null)
                user.Sobrenome = updateUser.Sobrenome;
            if (updateUser.Email != null)
                user.Email = updateUser.Email;
            if (updateUser.Senha != null)
                user.Senha = updateUser.Senha;
            if (updateUser.NivelAcesso != null)
                user.NivelAcesso = (Acesso)Enum.Parse(typeof(Acesso), updateUser.NivelAcesso);

            _context.Update(user);
            await _context.SaveChangesAsync();

            response.Data = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
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

    public async Task<ResponseModel<UserModel>> DeleteUser(int id)
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

            _context.Remove(user);
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

    public ResponseModel<UserModel> ValidateNewUser(UserDTO newUser)
    {
        ResponseModel<UserModel> response = new ResponseModel<UserModel>();
        
        if (newUser.Nome == null)
        {
            response.Data = null;
            response.Message = "Nome é obrigatório";
            response.Status = false;
        }
        if (newUser.Sobrenome == null)
        {
            response.Data = null;
            response.Message = response.Message + ", Sobrenome é obrigatório";
            response.Status = false;
        }
        if (newUser.Email == null)
        {
            response.Data = null;
            response.Message = response.Message + ", Email é obrigatório";
            response.Status = false;
        }
        if (newUser.Senha == null)
        {
            response.Data = null;
            response.Message = response.Message + ", Senha é obrigatório";
            response.Status = false;
        }
        if (newUser.NivelAcesso == null)
        {
            response.Data = null;
            response.Message = response.Message + ", NivelAcesso é obrigatório COMUM ou ADMIN";
            response.Status = false;
        }

        return response;
    }

    public ResponseModel<UserModel> ValidateUpdateUser(UserDTO updateUser)
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