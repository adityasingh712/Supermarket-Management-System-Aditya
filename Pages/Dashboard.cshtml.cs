using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;

namespace SupermarketManagementAditya.Pages
{
    public class DashboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DashboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int TotalProducts { get; set; }
        public decimal TotalRevenue { get; set; }
        public int ExpiringSoon { get; set; }
        public int TotalSuppliers { get; set; }

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Accounts/Login");
            }

            TotalProducts = _context.Products.Count();
            TotalRevenue = _context.Sales.Sum(x => x.TotalAmount);

            DateTime today = DateTime.Today;
            DateTime nextMonth = today.AddDays(30);

            ExpiringSoon = _context.Products
                .Count(x => x.ExpiryDate >= today && x.ExpiryDate <= nextMonth);

            TotalSuppliers = _context.Products
                .Select(x => x.Supplier)
                .Distinct()
                .Count();

            return Page();
        }
    }
}