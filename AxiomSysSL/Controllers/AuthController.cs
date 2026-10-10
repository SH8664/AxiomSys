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
        try
        {
            await _authService.RegisterAsync(dto);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto dto)
    {
        var token = await _authService.LoginAsync(dto);
        if (token == null) return Unauthorized("Invalid email or password");
        return Ok(token);
    }


}
