using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class Cart
    {
        [Key]
        [Column("CartID")]
        public int Id { get; set; }
        [Column("StoreID")]
        public int StoreId { get; set; }
        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [Column("DeliveryAddress")]
        public string Delivery { get; set; }
        [Column("DeliveryNoDiacritic")]
        public string? DeliveryNoDiacritic { get; set; }
        [Column("Phone")]
        [Range(0000000000, 9999999999)]
        public int? PhoneNumber { get; set; }
        [Column("TimeOrder")]
        public DateTime? TimeOrder { get; set; }
        [Range(1, 2)]
        public double? Status { get; set; }
        public ICollection<DetailCart> DetailCarts { get; set; }

    }
}
