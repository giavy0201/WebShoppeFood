namespace DAL.Non_Repository.MenuRepo
{
    public class ViewMenuFoodOfStore
    {
        public int StoreID { get; set; }
        public string StoreName { get; set; }
        public string MenuName { get; set; }
        public string FoodName { get; set; }
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
