using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Store
    {
        [Key]
        [Column("StoreID")]
        public int Id { get; set; }
        [Required]
        [Column("StoreName")]
        [MaxLength(150)]
        public string Name { get; set; }
        [Column("NameNoDiacritic")]
        [MaxLength(150)]
        public string? NameNoDiacritic { get; set; }
        [Column("StoreImage")]
        public string? Image { get; set; }
        [Column("TimeOpen")]
        public TimeSpan TimeOpen { get; set; }
        [Column("TimeClose")]
        public TimeSpan TimeClose { get; set; }
        [Column("PreferentialStore")]
        [MaxLength(50)]
        public string? Preferential { get; set; }
        [Column("StarEvaluateStore")]
        [Range(0, 5)]
        public double StarEvaluate { get; set; }
        [Required]
        [Column("StoreAddress")]
        [MaxLength(150)]
        public string Address { get; set; }
        [Column("AddressNoDiacritic")]
        [MaxLength(150)]
        public string? AddressNoDiacritic { get; set; }
        public int WardID { get; set; }
        [ForeignKey("WardID")]
        public Ward Ward { get; set; }
        public int ContentID { get; set; }
        [ForeignKey("ContentID")]
        public ContentProduct ContentProduct { get; set; }
        [Column("AddorUpdateBy")]
        [MaxLength(50)]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }

        public ICollection<ListMenu> ListMenu { get; set; }
        public ICollection<CollectionStore> CollectionStores { get; set; }
        public ICollection<AccountStore> AccountStores { get; set; }
        public ICollection<Comment> Comments { get; set; }

    }
}
