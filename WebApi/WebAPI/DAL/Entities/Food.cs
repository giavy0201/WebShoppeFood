using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        [Column("NameFood")]
        public string Name { get; set; }
        [Column("NameNoDiacritic")]
        [MaxLength(100)]
        public string? NameNoDiacritic { get; set; }
        [Column("imgFood")]
        public string? Image { get; set; }
        [Required]
        [Column("PriceFood")]
        public double Price { get; set; }
        [Column("DiscountFood")]
        public int? Discount { get; set; }
        [Column("DescriptionFood")]
        [MaxLength(500)]
        public string? Description { get; set; }
        public int MenuID { get; set; }
        [ForeignKey("MenuID")]
        public ListMenu ListMenu { get; set; }
        [Column("AddorUpdateBy")]
        [MaxLength(50)]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public ICollection<DetailCart> DetailCarts { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
