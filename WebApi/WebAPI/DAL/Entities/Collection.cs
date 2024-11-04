using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Collection
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Column("CollectionName")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Column("CollectionImg")]
        public string? Image { get; set; }
        [Column("CollectionDes")]
        public string? Description { get; set; }
        [ForeignKey("CategoryID")]
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public CategoryProduct? CategoryProduct { get; set; }
        public int CityID { get; set; }
        [ForeignKey("CityID")]
        public City? City { get; set; }

        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
        public List<CollectionStore> collectionStores { get; set; }
    }
}
