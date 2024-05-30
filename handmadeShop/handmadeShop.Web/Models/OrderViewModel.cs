using handmadeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace handmadeShop.Web.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; }

        [Required(ErrorMessage = "The Address filed is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The Phone Number field is required.")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter a valid phone number.")]
        [StringLength(8, ErrorMessage = "The Phone Number exceeds the required length")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The Last Name is Required")]
        public string LastName { get; set; }
        public string Appartment { get; set; }
        [Required(ErrorMessage = "The City is Required")]
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Country { get; set; }
        [Required(ErrorMessage = "The Email is Required")]
        public string Email { get; set; }
        public decimal TotalSumToPay { get; set; }
        public DateTime BuyingTime { get; set; }

        public ICollection<OrderItem> Items { get; set; }

        public OrderViewModel()
        {
            Items = new List<OrderItem>();
        }
    }
}