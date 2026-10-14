using AxiomSysPortal.Models;
using AxiomSysPortal.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using AxiomSysPortal.Pages.Localization;

namespace AxiomSysPortal.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly AuthApiService _auth;
        public ISharedLocalizer Localizer { get; }
        public LoginModel(AuthApiService auth, ISharedLocalizer localizer) { Localizer = localizer; _auth = auth; }


        [BindProperty] public UserLoginDto Input { get; set; } = new();
        [TempData] public string? Success { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid) return Page();

            var (token, error) = await _auth.LoginAsync(Input);
            if (token == null)
            {
                ModelState.AddModelError("", error ?? Localizer["37"]);
                return Page();
            }

            var identity = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, Input.Email),
            new Claim("access_token", token)          
        }, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties { IsPersistent = true });

            return LocalRedirect(Url.IsLocalUrl(returnUrl) ? returnUrl! : "/");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("Login");
        }
    }
}
