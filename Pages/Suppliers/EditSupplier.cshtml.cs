using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;

namespace SupermarketManagementAditya.Pages.Suppliers
{
    public class EditSupplierModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditSupplierModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string OldSupplierName { get; set; } = "";

        [BindProperty]
        public string NewSupplierName { get; set; } = "";

        public string Message { get; set; } = "";

        public IActionResult OnGet(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToPage("/Suppliers/SupplierIndex");
            }

            OldSupplierName = name;
            NewSupplierName = name;

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(NewSupplierName))
            {
                Message = "Supplier name is required";
                return Page();
            }

            var products = _context.Products
                .Where(x => x.Supplier == OldSupplierName)
                .ToList();

            foreach (var product in products)
            {
                product.Supplier = NewSupplierName;
            }

            _context.SaveChanges();

            return RedirectToPage("/Suppliers/SupplierIndex");
        }
    }
}