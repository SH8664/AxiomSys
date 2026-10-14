using AxiomSysBL.Models.Auth;

namespace AxiomSysBL.IServices.IAuthServices
{
    public interface IAuthService
    {
        Task RegisterAsync(UserRegisterDto dto);
        Task<string?> LoginAsync(UserLoginDto dto);
        string generateToken(string userId);
    }
}
