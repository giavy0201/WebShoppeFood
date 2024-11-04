using BLL.Models.DTOs;
using BLL.Models.DTOs.Food;
using BLL.Models.DTOs.ListMenu;
using BLL.Models.Request;
using BLL.Models.Request.Food;
using BLL.Models.Request.ListMenu;
using DAL.Entities;

namespace BLL.IService
{
    public interface IMenuService
    {
        Task<IEnumerable<ListMenuDtos>> ListMenuOfStore(int StoreID);
        Task<IEnumerable<ListMenuFoodOfStoreDtos>> ListMenuFoodOfStore(int StoreID);
        Task<IEnumerable<FoodDtos>> ListFoodOfMenu(int MenuID);
        Task<FoodDtos> DeatailFood(int FoodID);
        //admin store
        Task<List<ListMenuSystem>> GetListMenuByStore(int StoreID);
        Task<ListMenuSystem> DetailMenu(int MenuID);
        Task<ModelApiRequest> AddMenu(AddMenuRequest model);
        Task<ModelApiRequest> UpdateMenu(UpdateMenuRequest model);
        Task<ListMenu> DeleteMenu(int MenuID);
        Task<ModelApiRequest> UpdateMenuStatus(int MenuID);
        Task<List<FoodSystem>> GetListFoodByStore(int StoreID);
        Task<List<FoodSystem>> GetListFoodByMenu(int MenuID);
        Task<FoodSystem> DetailFoodAdmin(int FoodID);
        Task<ModelApiRequest> AddFood(AddFoodRequest model);
        Task<ModelApiRequest> UpdateFood(UpdateFoodRequest model);
        Task<Food> DeleteFood(int FoodID);
        Task<ModelApiRequest> UpdateFoodStatus(int FoodID);
        Task<List<FoodSystem>> SearchProduct(SelectProductRequest reqProduct);
    }
}
