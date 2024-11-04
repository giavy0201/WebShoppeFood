using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ContentProduct
    {
        [Key]
        [Column("ContentID")]
        public int Id { get; set; }
        [Required]
        [Column("ContentName")]
        [MaxLength(100)]
        public string Name { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public CategoryProduct CategoryProduct { get; set; }
        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public ICollection<Store> Stores { get; set; }

    }
}
