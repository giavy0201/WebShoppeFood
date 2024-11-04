namespace BLL.Models.DTOs.Cart
{
    public class CartDtos
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string CustomerId { get; set; }
        public string Delivery { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime? TimeOrder { get; set; }
        public double? Status { get; set; }
    }
}
