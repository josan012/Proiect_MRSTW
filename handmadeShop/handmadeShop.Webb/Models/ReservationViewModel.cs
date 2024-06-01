using handmadeShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace handmadeShop.Web.Models
{
    public class ReservationViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        public DateTime ReservationDate { get; set; } = DateTime.Now;   

        [Required]

        public string ReservationTime { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage ="The Phone Number is required")]
        public string PhoneNumber { get; set; }

        public string Message { get; set; }

        // Relationship with the user
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ReservationDate.Date < DateTime.Now.Date)
            {
                yield return new ValidationResult("Reservation date must be in the future.", new[] { "ReservationDate" });
            }

            if (ReservationDate.DayOfWeek == DayOfWeek.Saturday || ReservationDate.DayOfWeek == DayOfWeek.Sunday)
            {
                yield return new ValidationResult("Reservation date cannot be on a Saturday or Sunday.", new[] { "ReservationDate" });
            }

            TimeSpan startTime = new TimeSpan(8, 0, 0); 
            TimeSpan endTime = new TimeSpan(21, 0, 0);  

            if (!DateTime.TryParseExact(ReservationTime, "h:mmtt", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedTime))
            {
                yield return new ValidationResult("Invalid reservation time format.", new[] { "ReservationTime" });
            }

            var reservationTime = parsedTime.TimeOfDay;

            if (reservationTime < startTime || reservationTime >= endTime)
            {
                yield return new ValidationResult("Reservation time must be between 8 am and 9 pm.", new[] { "ReservationTime" });
            }
        }


    }
}
