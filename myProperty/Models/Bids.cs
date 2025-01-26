using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myProperty.Models
{
    [Table("Bid")] // Explicitly map to the "Bid" table
    public class Bid
    {
        [Key]
        public int BidiD { get; set; } // Primary Key

        [Required]
        public int AuctionID { get; set; } // Foreign Key to Auction

        [Required]
        public int BuyerID { get; set; } // Foreign Key to User (Buyer)

        [Required(ErrorMessage = "Bid Amount is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Bid Amount must be greater than 0.")]
        public decimal BidAmount { get; set; }

        [Required(ErrorMessage = "Bid Date is required.")]
        public DateTime BidDate { get; set; }

        // Navigation Properties
        public virtual Auction Auction { get; set; }
        public virtual User User { get; set; } // Navigation property
    }
}
