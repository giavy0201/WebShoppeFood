namespace BLL.Models.DTOs.Cart
{
    public class DetailCartDtos
    {
        public int FoodId { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int CartId { get; set; }
        public double TotalMoney
        {
            get { return Quantity * Price; }
        }
    }
}
