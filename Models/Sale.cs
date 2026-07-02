using System.ComponentModel.DataAnnotations;

namespace SupermarketManagementAditya.Models
{
    public class Sale
    {
        public int Id { get; set; }

        [Required]
        public string ProductName { get; set; }

        [Required]
        public int QuantitySold { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public DateTime SaleDate { get; set; } = DateTime.Now;
    }
}