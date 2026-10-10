using AxiomSysPortal.Models;
using AxiomSysPortal.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AxiomSysPortal.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly AuthApiService _auth;
        public RegisterModel(AuthApiService auth) => _auth = auth;

        [BindProperty] public UserRegisterDto Input { get; set; } = new();


        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var (ok, error) = await _auth.RegisterAsync(Input);
            if (ok)
            {
                TempData["Success"] = "Account created. Please log in.";
                return RedirectToPage("Login");
            }

            ModelState.AddModelError("", error ?? "Registration failed");
            return Page();
        }

    }
}
