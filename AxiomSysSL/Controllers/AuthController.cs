using AxiomSysBL.IServices.IAuthServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AxiomSysSL.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    IAuthService _authService;
    private readonly UserManager<IdentityUser> _userManager;
    public AuthController(IAuthService authService, UserManager<IdentityUser> userManager)
    {
        _authService = authService;
        _userManager = userManager;
    }




    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(UserRegisterDto dto)
    {
        await _authService.RegisterAsync(dto);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        return Ok(result);
    }


}
