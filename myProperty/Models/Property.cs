using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace myProperty.Models
{
    [Table("Property")]
    public class Property
    {
        [Key]
        public int PropertyID { get; set; } // Primary Key

        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [StringLength(200)]
        public string ImageURL { get; set; }

        [StringLength(200)]
        public string VideoURL { get; set; }

        [Required]
        [ForeignKey("User")] // Explicitly map SellerID as the foreign key for User
        public int SellerID { get; set; }

        // Navigation Property
        public virtual User User { get; set; } // Seller
    }
}
