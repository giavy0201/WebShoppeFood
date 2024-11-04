namespace BLL.Model.Cart
{
    public class CartModel
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public double FoodPrice { get; set; }
        public int FoodQuantity { get; set; }
        public double TotalMoney
        {
            get { return FoodQuantity * FoodPrice; }
        }
        public int StoreID { get; set; }
        public CartModel(int foodId, string foodName, double foodPrice, int storeID)
        {
            FoodID = foodId;
            FoodName = foodName;
            FoodPrice = foodPrice;
            FoodQuantity = 1;
            StoreID = storeID;
        }
    }
}
