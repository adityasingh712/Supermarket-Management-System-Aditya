using System.ComponentModel.DataAnnotations;

namespace SupermarketManagementAditya.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Supplier is required")]
        public string Supplier { get; set; }

        [Required(ErrorMessage = "Barcode is required")]
        public string Barcode { get; set; }

        [Required]
        [Range(0.01, 99999, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "Quantity cannot be negative")]
        public int Quantity { get; set; }

        // Automatic stock availability status
        public string StockStatus
        {
            get
            {
                if (Quantity == 0)
                    return "Out of Stock";
                else if (Quantity <= 5)
                    return "Low Stock";
                else
                    return "In Stock";
            }
        }

        [Required(ErrorMessage = "Manufacturing date is required")]
        [DataType(DataType.Date)]
        public DateTime ManufacturingDate { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        [DataType(DataType.Date)]
        public DateTime ExpiryDate { get; set; }

        public string? ImagePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}