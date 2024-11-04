using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CategoryProduct
    {
        [Key]
        [Column("CategoryID")]
        public int Id { get; set; }
        [Required]
        [Column("CategoryName")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public ICollection<ContentProduct> ContentProducts { get; set; }
        public ICollection<Collection> Collections { get; set; }

    }
}
