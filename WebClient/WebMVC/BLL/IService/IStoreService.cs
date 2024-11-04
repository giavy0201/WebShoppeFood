using BLL.Model;
using BLL.Model.Cart;
using BLL.Model.ModelRequest;
using BLL.Model.ModelStoreDtos;

namespace BLL.IService
{
    public interface IStoreService
    {
        Task<List<ListMenuDtos>> ListMenuStore(int StoreID);
        Task<List<FoodDtos>> ListFoodOfMenu(int MenuID);
        Task<ApiResponse<List<ListStoreOfCollecDtos>>> StoreByCollection(ReqListStoreOfCollec reqStoreCollection);
        Task<ApiResponse<List<StoreDtos>>> ListStoreSearch(ReqSearch reqSearch);
        Task<ApiResponse<List<StoreDtos>>> ListStorePreferential(ReqStorePreferential reqStorePre);
        Task<ApiResponse<List<StoreDtos>>> ListStoreByDistrict(ReqStoreMenuBot reqStoreMenuBot);
        Task<StoreDtos> DetailStore(int StoreID);
        Task<FoodDtos> DetailFood(int FoodID);
        Task<List<FoodDtos>> ListFoodOfStore(int StoreID);
        Task<ReqOrderMess> OrderFood(ReqOrder reqOrder);
        Task<StoreDtos> GetOrderByFoodAndStoreId(int foodId, int storeId,string orderCode);
    }
}
