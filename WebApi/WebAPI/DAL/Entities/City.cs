using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class City
    {
        [Key]
        [Column("CityID")]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Column("CityName")]
        public string Name { get; set; }
        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public ICollection<District> Districts { get; set; }
        public ICollection<Collection> Collections { get; set; }

    }
}
