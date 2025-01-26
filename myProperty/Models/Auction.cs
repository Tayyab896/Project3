using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myProperty.Models
{
    public class Auction
    {
        [Key]
        public int AuctionID { get; set; } // Primary Key

        [Required]
        public int PropertyID { get; set; } // Foreign Key

        [Required(ErrorMessage = "Start Date is required.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Minimum Bid Increment is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Minimum Bid Increment must be greater than 0.")]
        public decimal MinBidIncrement { get; set; }

        [Required(ErrorMessage = "Reserve Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Reserve Price must be greater than 0.")]
        public decimal ReservePrice { get; set; }

        // Navigation Properties
        public virtual Property Property { get; set; } // Related property
        public virtual ICollection<Bid> Bid { get; set; } // Related bids
    }
}
