using AxiomSysBL.IServices.IAuthServices;
using Microsoft.AspNetCore.Mvc;

namespace AxiomSysSL.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{

    IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpGet]
    public string GetToken(string userId)
    {
        return _authService.generateToken(userId);
    }
}
