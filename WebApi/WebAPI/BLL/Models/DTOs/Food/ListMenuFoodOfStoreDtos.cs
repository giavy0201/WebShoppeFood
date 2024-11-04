namespace BLL.Models.DTOs
{
    public class ListMenuFoodOfStoreDtos
    {
        public string Name { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public int? Discount { get; set; }
        public string? Description { get; set; }
        public double? PriceDiscount
        {
            get
            {
                return Price - (Price * Discount * 0.01);
            }
        }
    }
}
