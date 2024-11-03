using BLL.Model;
using BLL.Model.Address;
using BLL.Model.Cart;
using BLL.Model.Store;
using Microsoft.AspNetCore.Mvc;

namespace BLL.IService
{
    public interface IStoreService
    {
        Task<ApiResponse<ViewLogin>> LoginSystemStore(ReqLogin modelLogin);
        Task<StoreDtos> DetailStore(int StoreID);
        Task<List<CityDtos>> GetListCity();
        Task<List<DistrictDtos>> GetListDistrictByCity(int CityID);
        Task<List<WardDtos>> GetListWardByDistrict(int DistrictID);
        Task<ApiResponse<string>> UpdateInfoStore(ReqUpdateStore modelstore);
        Task<ApiResponse<ApiRequest>> UpdateStatusStore(int StoreID);
        Task<ViewIDLocation> LocationIDByWard(int WardID);
        Task<ApiResponse<List<CartDtos>>> ListCartOrderTodayByStore(ModelSelectCart req);
        Task<CartDtos> SelectCartByID(int CartID);
        Task<double> TotalMoneyToday(int StoreID);
        Task<ApiRequest> UpdateStatusOrder(SetStatusCart setReq);
        Task<List<CartDtos>> ListCartByStore(ModelSelectCart model);
      
    }
}
