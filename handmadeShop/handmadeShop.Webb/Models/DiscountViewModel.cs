using System;
using System.ComponentModel.DataAnnotations;

namespace handmadeShop.Web.Models
{
    public class DiscountViewModel
    {
        public int Id { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Percentage must be a numeric value.")]
        [Range(0, 100, ErrorMessage = "Percentage must be between 0 and 100.")]
        public decimal Percentage { get; set; }
        public DateTime SetTime { get; set; } = DateTime.Now;   
        public DateTime ExpirationTime { get; set; }
    }
}