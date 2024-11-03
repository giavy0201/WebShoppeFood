using BLL.Model.Cart;
using BLL.Model.Food;

namespace WebSystemStore.Models
{
    public class ListEmpty
    {
        public static List<CartDtos> ListCart
        {
            get { return new List<CartDtos>(); }
        }

        public static List<FoodDtos> ListFood
        {
            get { return new List<FoodDtos>(); }
        }
    }
}
