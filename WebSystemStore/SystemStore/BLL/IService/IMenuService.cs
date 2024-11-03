using BLL.Model;
using BLL.Model.Menu;

namespace BLL.IService
{
    public interface IMenuService
    {
        Task<List<MenuDtos>> ListMenuStore(int StoreID);
        Task<int> TotleFoodInMenu(int MenuID);
        Task<ApiResponse<ApiRequest>> UpdateStatusMenu(int MenuID);
        Task<ApiResponse<string>> InsertMenu(ReqCreateMenu modelMenu);
        Task<MenuDtos> DetailMenu(int MenuID);
        Task<ApiResponse<string>> UpdateMenu(ReqUpdateMenu modelMenu);
        Task<ApiResponse<string>> DeleteMenu(int MenuID);

    }
}
