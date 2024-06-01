﻿using System.ComponentModel.DataAnnotations;

namespace handmadeShop.Web.Models
{
    public class PasswordModel
    {
        [Required]
        [Display(Name = "New Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your current password")]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string CurrentPassword { get; set; }

        [Required]
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "The new password does not match the confirmation password!")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}