using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Products
{
    public class EditProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditProductModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public IActionResult OnGet(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Accounts/Login");
            }

            var product = _context.Products.FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return RedirectToPage("/Products/ProductList");
            }

            Product = product;

            return Page();
        }

        public IActionResult OnPost()
        {
            var productFromDb = _context.Products.FirstOrDefault(x => x.Id == Product.Id);

            if (productFromDb == null)
            {
                return RedirectToPage("/Products/ProductList");
            }

            productFromDb.ProductName = Product.ProductName;
            productFromDb.Category = Product.Category;
            productFromDb.Supplier = Product.Supplier;
            productFromDb.Barcode = Product.Barcode;
            productFromDb.Price = Product.Price;
            productFromDb.Quantity = Product.Quantity;
            productFromDb.ManufacturingDate = Product.ManufacturingDate;
            productFromDb.ExpiryDate = Product.ExpiryDate;

            if (ImageFile != null)
            {
                string uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    ImageFile.CopyTo(stream);
                }

                productFromDb.ImagePath = "/uploads/" + fileName;
            }

            _context.SaveChanges();

            return RedirectToPage("/Products/ProductList");
        }
    }
}