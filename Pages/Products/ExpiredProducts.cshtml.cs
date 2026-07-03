using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Products
{
    public class ExpiredProductsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ExpiredProductsModel(ApplicationDbContext context)
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

            Products = _context.Products
                .Where(x => x.ExpiryDate < today)
                .OrderBy(x => x.ExpiryDate)
                .ToList();

            return Page();
        }
    }
}