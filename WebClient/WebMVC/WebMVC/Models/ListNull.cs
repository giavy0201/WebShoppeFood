using BLL.Model.ModelStoreDtos;
using BLL.Model.ProductDtos;

namespace WebMVC.Models
{
    public class ListNull
    {
        public static List<StoreDtos> ListStore
        {
            get { return new List<StoreDtos>(); }
        }

        public static List<CollectionDtos> ListCollections
        {
            get { return new List<CollectionDtos>(); }
        }
        public static List<FoodDtos> ListFood
        {
            get { return new List<FoodDtos>(); }
        }
    }
}
