using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Sales
{
    public class SalesIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SalesIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> Products { get; set; } = new();
        public List<Sale> SalesHistory { get; set; } = new();

        [BindProperty]
        public int SelectedProductId { get; set; }

        [BindProperty]
        public int QuantitySold { get; set; }

        public string Message { get; set; } = "";

        public decimal TotalRevenue { get; set; }
        public int TotalItemsSold { get; set; }

        public void OnGet()
        {
            LoadData();
        }

        public IActionResult OnPost()
        {
            var product = _context.Products
                .FirstOrDefault(x => x.Id == SelectedProductId);

            if (product == null)
            {
                LoadData();
                Message = "Product not found";
                return Page();
            }

            if (QuantitySold <= 0)
            {
                LoadData();
                Message = "Quantity must be greater than 0";
                return Page();
            }

            if (QuantitySold > product.Quantity)
            {
                LoadData();
                Message = "Stock is not enough";
                return Page();
            }

            product.Quantity = product.Quantity - QuantitySold;

            Sale sale = new Sale
            {
                ProductName = product.ProductName,
                QuantitySold = QuantitySold,
                TotalAmount = product.Price * QuantitySold
            };

            _context.Sales.Add(sale);
            _context.SaveChanges();

            LoadData();

            Message = "Sale saved successfully";

            return Page();
        }

        public void LoadData()
        {
            Products = _context.Products.ToList();
            SalesHistory = _context.Sales.ToList();

            TotalRevenue = _context.Sales.Sum(x => x.TotalAmount);
            TotalItemsSold = _context.Sales.Sum(x => x.QuantitySold);
        }
    }
}