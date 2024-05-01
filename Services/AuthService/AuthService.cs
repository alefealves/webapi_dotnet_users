using Microsoft.EntityFrameworkCore;
using UsersManager.Data;
using UsersManager.Models;
using UsersManager.Services.SenhaService;

namespace UsersManager.Services.AuthService;

public class AuthService : IAuthInterface
{
    private readonly AppDbContext _context;
    private readonly ISenhaInterface _senhaInterface;
    public AuthService(AppDbContext context, ISenhaInterface senhaInterface)
    {
        _context = context;
        _senhaInterface = senhaInterface;
    }
    
    public async Task<ResponseModel<UserCreateDTO>> Register(UserCreateDTO userRegister)
    {
        ResponseModel<UserCreateDTO> responseService = new ResponseModel<UserCreateDTO>();
        try
        {
            responseService = await UserEmailExist(userRegister);
            if (!responseService.Status)
            {
                responseService.Data = null;
                responseService.Message = "Já existe um usuário cadastrado com esse email.";
                responseService.Status = false;
                return responseService;
            }

            _senhaInterface.CriarSenhaHash(userRegister.Senha, out byte[] senhaHash, out byte[] senhaSalt);

            UserModel user = new UserModel()
            {
                Nome = userRegister.Nome,
                Sobrenome = userRegister.Sobrenome,
                Email = userRegister.Email,
                NivelAcesso = userRegister.NivelAcesso,
                SenhaHash = senhaHash,
                SenhaSalt = senhaSalt
            };

            _context.Add(user);
            await _context.SaveChangesAsync();

            responseService.Message = "Usuário criado com sucesso!";
            
        }
        catch (Exception ex)
        {
            responseService.Data = null;
            responseService.Message = ex.Message;
            responseService.Status = false;
        }

        return responseService;
    }
    
    public async Task<ResponseModel<string>> Login(UserDTOlogin userLogin)
    {
        ResponseModel<string> responseService = new ResponseModel<string>();

        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userLogin.Email);
            if(user == null)
            {
                responseService.Message = "Email não cadastrado.";
                responseService.Status = false;
                return responseService;
            }

            if (!_senhaInterface.VerificaSenhaHash(userLogin.Senha, user.SenhaHash, user.SenhaSalt))
            {
                responseService.Message = "Senha inválida.";
                responseService.Status = false;
                return responseService;
            }

            var token = _senhaInterface.CriarToken(user);

            responseService.Data = token;
            responseService.Message = "Usuário logado com sucesso!";
        }catch (Exception ex)
        {
            responseService.Data = null;
            responseService.Message = ex.Message;
            responseService.Status = false;
        }
        
        return responseService;
    }
    
    public async Task<ResponseModel<UserCreateDTO>> UserEmailExist(UserCreateDTO userRegister)
    {
        ResponseModel<UserCreateDTO> responseService = new ResponseModel<UserCreateDTO>();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == userRegister.Email);
        if (user != null)
        {
            responseService.Data = null;
            responseService.Status = false;
            responseService.Message = "Email já cadastrado para outro usuário!";
        }
        else
        {
            responseService.Data = null;
            responseService.Status = true;
            responseService.Message = "Success";
        }
        return responseService;
    }
    
}