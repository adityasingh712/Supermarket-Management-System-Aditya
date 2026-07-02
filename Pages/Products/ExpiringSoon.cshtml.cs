using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Products
{
    public class ExpiringSoonModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpiringSoonModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new();

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Accounts/Login");
            }

            DateTime today = DateTime.Today;
            DateTime nextMonth = today.AddDays(30);

            Products = _context.Products
                .Where(x => x.ExpiryDate <= nextMonth)
                .OrderBy(x => x.ExpiryDate)
                .ToList();

            return Page();
        }
    }
}