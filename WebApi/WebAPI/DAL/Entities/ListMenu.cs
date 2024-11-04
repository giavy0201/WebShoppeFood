using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class ListMenu
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("MenuName")]
        [MaxLength(100)]
        public string Name { get; set; }
        [ForeignKey("StoreID")]
        public int StoreID { get; set; }
        public Store Store { get; set; }
        [Column("AddorUpdateBy")]
        [MaxLength(50)]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
