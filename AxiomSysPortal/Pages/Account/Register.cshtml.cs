using AxiomSysPortal.Models;
using AxiomSysPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using AxiomSysPortal.Pages.Localization;
namespace AxiomSysPortal.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AuthApiService _auth;
        private readonly ISharedLocalizer Localizer;
        public RegisterModel(AuthApiService auth, ISharedLocalizer localizer)
        {
            _auth = auth;
            Localizer = localizer;
        }
        [BindProperty] public UserRegisterDto Input { get; set; } = new();


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var (ok, error) = await _auth.RegisterAsync(Input);
            if (ok)
            {
                TempData["Success"] = Localizer["35"];
                return RedirectToPage("Login");
            }

            ModelState.AddModelError("", error ?? Localizer["34"]);
            return Page();
        }

    }
}
