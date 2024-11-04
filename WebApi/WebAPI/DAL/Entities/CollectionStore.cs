using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class CollectionStore
    {
        [Key]
        [Column("CollectionStoreID")]
        public int Id { get; set; }
        [ForeignKey("CollectionID")]
        public int? CollectionID { get; set; }
        public Collection? Collection { get; set; }
        [ForeignKey("StoreID")]
        public int? StoreID { get; set; }
        public Store? Store { get; set; }
        [Column("AddorUpdateBy")]
        public string? AdminName { get; set; }
        [Column("AddorUpdateAt")]
        public DateTime? AdminTime { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
    }
}
