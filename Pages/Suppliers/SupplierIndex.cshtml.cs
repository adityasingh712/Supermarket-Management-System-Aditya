using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Suppliers
{
    public class SupplierIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SupplierIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<SupplierGroup> SupplierGroups { get; set; } = new();

        public void OnGet()
        {
            var products = _context.Products.ToList();

            SupplierGroups = products
                .GroupBy(x => x.Supplier)
                .Select(group => new SupplierGroup
                {
                    SupplierName = group.Key,
                    Products = group.ToList()
                })
                .ToList();
        }

        public class SupplierGroup
        {
            public string SupplierName { get; set; } = "";
            public List<Product> Products { get; set; } = new();
        }
    }
}