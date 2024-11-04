using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;

namespace DAL.Non_Repository.MenuRepo
{
    public interface IMenuRepository
    {
        IEnumerable<ListMenu> ListMenuOfStore(int StoreID);
        IEnumerable<Food> ListFoodOfMenu(int MenuID);
        IEnumerable<ViewMenuFoodOfStore> ListMenuByStore(int StoreID);
        IEnumerable<int> GetListStoreByKey(string  keyword);
        ViewFoodDtos DeatailFood(int FoodID);
        //admin store
        Task<List<ListMenu>> GetListMenuByStore(int StoreID);
        Task<ListMenu> AddMenu(AddMenuReq addMenuReq);
        Task<ListMenu> UpdateMenu(UpdateMenuReq updateMenuReq);
        Task<ListMenu> DetailMenu(int MenuID);
        Task<ListMenu> DeleteMenu(int MenuID);
        Task<ListMenu> UpdateStatusMenu(int MenuID);
        Task<List<Food>> GetListFoodByStore(int StoreID);
        Task<List<Food>> GetListFoodByMenu(int MenuID);
        Task<Food> AddFood(AddFoodReq addFoodReq);
        Task<Food> UpdateFood(UpdateFoodReq updateFoodReq);
        Task<Food> DetailFoodAdmin(int FoodID);
        Task<Food> DeleteFood(int FoodID);
        Task<Food> UpdateStatusFood(int FoodID);
        Task<List<Food>> SearchProduct(SearchProductReq request);
    }
}
