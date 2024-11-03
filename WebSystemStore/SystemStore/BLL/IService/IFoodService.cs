using BLL.Model;
using BLL.Model.Food;

namespace BLL.IService
{
    public interface IFoodService
    {
        Task<List<FoodDtos>> ListFoodByMenu(int MenuID);
        Task<ApiResponse<ApiRequest>> UpdateStatusFood(int FoodID);
        Task<FoodDtos> DetailFood(int FoodID);
        Task<ApiResponse<string>> UpdateFood(ReqUpdateFood modelfood);
        Task<ApiResponse<string>> InsertFood(ReqInsertFood modelfood);
        Task<ApiResponse<string>> DeleteFood(int FoodID);
        Task<List<FoodDtos>> ListFoodByStore(int StoreID);
        Task<List<FoodDtos>> ListFoodSeach(ModelSearchProduct modelSearch);
    }
}
