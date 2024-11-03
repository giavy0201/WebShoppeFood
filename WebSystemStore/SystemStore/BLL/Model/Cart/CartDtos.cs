namespace BLL.Model.Cart
{
    public class CartDtos
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public string CustomerId { get; set; }
        public string Delivery { get; set; }
        public int? PhoneNumber { get; set; }
        public DateTime TimeOrder { get; set; }
        public double? Status { get; set; }
        public List<SelectCart> DetailCarts { get; set; }
    }

    public class SelectCart
    {
        public int Id { get; set; }
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int CartID { get; set; }
        public double TotalMoney => Quantity * Price;
    }
}
