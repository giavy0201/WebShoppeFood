using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class District
    {
        [Key]
        [Column("DistrictID")]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        [Column("DistrictName")]
        public string Name { get; set; }
        public int CityID { get; set; }
        [ForeignKey("CityID")]
        public City City { get; set; }
        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public ICollection<Ward> Ward { get; set; }

    }
}
