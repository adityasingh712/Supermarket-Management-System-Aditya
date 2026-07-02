using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Products
{
    public class ProductListModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProductListModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new();

        [BindProperty]
        public int DeleteId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchText { get; set; } = "";

        public IActionResult OnGet()
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(SearchText))
            {
                query = query.Where(x =>
                    x.ProductName.Contains(SearchText) ||
                    x.Barcode.Contains(SearchText));
            }

            Products = query.ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            var product = _context.Products
                .FirstOrDefault(x => x.Id == DeleteId);

            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}