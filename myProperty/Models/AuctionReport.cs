using System.ComponentModel.DataAnnotations;

namespace myProperty.Models
{
    public class AuctionReport
    {
        [Key]
        public int ReportID { get; set; } // Primary Key

        [Required]
        public int AuctionID { get; set; } // Foreign Key

        [Required(ErrorMessage = "Winning Bid is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Winning Bid must be greater than 0.")]
        public decimal WinningBid { get; set; }

        [Required(ErrorMessage = "Final Sale Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Final Sale Price must be greater than 0.")]
        public decimal FinalSalePrice { get; set; }

        // Navigation Property
        public virtual Auction Auction { get; set; }
    }
}
