using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request
{
    public class CartRequest
    {
        [Required(ErrorMessage = "Bắt Buộc Phải Có Mã Cửa Hàng")]
        public int? StoreId { get; set; }
        public string CustomerId { get; set; }
        public string Delivery { get; set; }
        public int PhoneNumber { get; set; }
        public List<DetailCartRequest>? Details { get; set; }
    }

    public class DetailCartRequest
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }

}
