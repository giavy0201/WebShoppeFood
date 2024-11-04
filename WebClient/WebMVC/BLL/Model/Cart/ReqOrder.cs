namespace BLL.Model.Cart
{
    public class ReqOrder
    {
        public int StoreId { get; set; }
        public string CustomerId { get; set; }
        public string Delivery { get; set; }
        public string PhoneNumber { get; set; }
        public List<ReqDetailCart>? Details { get; set; } = new List<ReqDetailCart>();
    }
    public class ReqDetailCart
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
