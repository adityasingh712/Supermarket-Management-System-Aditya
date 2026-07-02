using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;
using System.ComponentModel.DataAnnotations;

namespace SupermarketManagementAditya.Pages.Accounts
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public RegisterInput Input { get; set; } = new();

        public string Message { get; set; } = "";

        public class RegisterInput
        {
            [Required]
            public string FirstName { get; set; } = "";

            [Required]
            public string LastName { get; set; } = "";

            [Required]
            [EmailAddress]
            public string Email { get; set; } = "";

            [Required]
            [MinLength(6)]
            public string Password { get; set; } = "";

            [Required]
            [Compare("Password")]
            public string ConfirmPassword { get; set; } = "";
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            if (_context.Users.Any(u => u.Email == Input.Email))
            {
                Message = "This email is already registered.";
                return Page();
            }

            var user = new User
            {
                FirstName = Input.FirstName,
                LastName = Input.LastName,
                Email = Input.Email,
                Password = Input.Password
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToPage("/Accounts/Login");
        }
    }
}