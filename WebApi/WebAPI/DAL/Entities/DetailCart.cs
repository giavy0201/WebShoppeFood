using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class DetailCart
    {
        [Key]
        [Column("DetailCartID")]
        public int Id { get; set; }
        public int FoodId { get; set; }
        [ForeignKey("FoodId")]
        public Food Food { get; set; }
        [Column("Quantity")]
        public int Quantity { get; set; }
        [Column("PriceFood")]
        public double Price { get; set; }
        public int CartID { get; set; }
        [ForeignKey("CartID")]
        public Cart Cart { get; set; }
    }
}
