using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using System.ComponentModel.DataAnnotations;

namespace SupermarketManagementAditya.Pages.Accounts
{
    public class LoginModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LoginModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public LoginInput Input { get; set; } = new();

        public string Message { get; set; } = "";

        public class LoginInput
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            public string Password { get; set; } = "";
        }

        public IActionResult OnPost()
        {
            var user = _context.Users
                .FirstOrDefault(x => x.Email == Input.Email && x.Password == Input.Password);

            if (user == null)
            {
                Message = "Invalid email or password";
                return Page();
            }

            HttpContext.Session.SetString("UserEmail", user.Email);
            HttpContext.Session.SetString("UserName", user.FirstName);

            return RedirectToPage("/Dashboard");
        }
    }
}