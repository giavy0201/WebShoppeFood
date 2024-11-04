using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Column("StarRating")]
        [Range(0, 5)]
        public double StarRating { get; set; } // Đánh giá sao cho bình luận (từ 0 đến 5)

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } // Add this property

        public string CustomerId { get; set; } // Reference to Customer

        // Navigation property to Customer
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; } // Add this line



        // Quan hệ với Store
        public int? StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        // Quan hệ với Food
        public int? FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food Food { get; set; }

        [Column("AddorUpdateBy")]
        [MaxLength(50)]
        public string? AdminName { get; set; }

        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }

        [Range(0, 2)]
        public int? Status { get; set; }
        [NotMapped] // Đánh dấu là không được lưu vào cơ sở dữ liệu
        public string FirstName { get; set; }

        [NotMapped] // Đánh dấu là không được lưu vào cơ sở dữ liệu
        public string LastName { get; set; }
    }
}
