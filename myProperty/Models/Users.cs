using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myProperty.Models
{
    public class User
    {
        [Key]
        public int UserID { get; set; } // Primary Key

        [Required(ErrorMessage = "Full Name is required.")]
        [StringLength(50, ErrorMessage = "Full Name cannot exceed 50 characters.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be between 8 and 50 characters.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "User Type is required.")]
        [StringLength(10, ErrorMessage = "User Type must be 'Buyer' or 'Seller'.")]
        public string UserType { get; set; }

        // Navigation Properties

        // If this user is a seller, they can have multiple properties
        public virtual ICollection<Property> Properties { get; set; }

        // If this user is a buyer, they can place multiple bids
        public virtual ICollection<Bid> Bids { get; set; }
    }
}
