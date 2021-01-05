using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingPlatform.Models
{
    public class Bookings
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Email Address can't be empty.")]
        [EmailAddress(ErrorMessage = "Email Address is not valid.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email Address is not valid.")]
        public string EmailAddress { get; set; }
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Date from can't be empty.")]
        public DateTime DateFrom { get; set; }

        [Required(ErrorMessage = "Date to can't be empty.")]
        public DateTime DateTo { get; set; }

        [Required(ErrorMessage = "Visitor/s can't be empty.")]
        [RegularExpression("([1-9][0-9]*)",ErrorMessage ="Vistor/s should be greater than 1.")]
        public int PersonNo { get; set; }

        [NotMapped]
        public virtual Rooms Room { get; set; }
    }
}