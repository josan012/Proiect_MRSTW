using System.ComponentModel.DataAnnotations;

namespace handmadeShop.Web.Models
{
    public class DeliveryCostViewModel
    {
        public int Id { get; set; }
        [RegularExpression(@"^\d*([.,]?\d+)?$", ErrorMessage = "Cost must be a numeric value.")]

        [Range(0, 1000, ErrorMessage = "Cost must be greater than or equal to 0.")]
        [Required]
        public decimal Cost { get; set; }
    }
}