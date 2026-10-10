namespace AxiomSysBL.IServices.IAuthServices
{
    public interface IAuthService
    {
        Task RegisterAsync(UserRegisterDto dto);
        Task<string?> LoginAsync(UserLoginDto dto);
        string generateToken(string userId);



    }
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }


}
