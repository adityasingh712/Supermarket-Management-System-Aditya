using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketManagementAditya.Data;
using SupermarketManagementAditya.Models;

namespace SupermarketManagementAditya.Pages.Products
{
    public class AddProductModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public AddProductModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Product Product { get; set; } = new();

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public string Message { get; set; } = "";

        public IActionResult OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Accounts/Login");
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToPage("/Accounts/Login");
            }

            if (string.IsNullOrEmpty(Product.ProductName))
            {
                Message = "Product name is required";
                return Page();
            }

            if (string.IsNullOrEmpty(Product.Barcode))
            {
                Message = "Barcode is required";
                return Page();
            }

            if (Product.Price <= 0)
            {
                Message = "Price must be greater than 0";
                return Page();
            }

            if (Product.Quantity < 0)
            {
                Message = "Quantity cannot be negative";
                return Page();
            }

            if (Product.ExpiryDate <= Product.ManufacturingDate)
            {
                Message = "Expiry date must be after manufacturing date";
                return Page();
            }

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

                Product.ImagePath = "/uploads/" + fileName;
            }

            _context.Products.Add(Product);
            _context.SaveChanges();

            return RedirectToPage("/Products/ProductList");
        }
    }
}