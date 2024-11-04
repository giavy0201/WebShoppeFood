using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Ward
    {
        [Key]
        [Column("WardID")]
        public int Id { get; set; }
        [Required]
        [Column("WardName")]
        public string? Name { get; set; }
        public int DistrictID { get; set; }
        [ForeignKey("DistrictID")]
        public District? District { get; set; }
        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }

        public ICollection<Store>? Stores { get; set; }

    }
}
