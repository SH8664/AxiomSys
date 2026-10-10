using System.ComponentModel.DataAnnotations;

namespace AxiomSysPortal.Models
{
    public class UserRegisterDto
    {
        [Required] public string UserName { get; set; } = "";
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    }

    public class UserLoginDto
    {
        [Required, EmailAddress] public string Email { get; set; } = "";
        [Required, DataType(DataType.Password)] public string Password { get; set; } = "";
    }
}
