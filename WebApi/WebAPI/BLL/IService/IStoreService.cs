using BLL.Models.DTOs;
using BLL.Models.DTOs.AccountStore;
using BLL.Models.Request;
using BLL.Models.Request.Store;

namespace BLL.IService
{
    public interface IStoreService
    {
        Task<StoreDtos> GetStoreById(int StoreID);
        Task<IEnumerable<StoreDtos>> ListStorePreByCityAndCate(StoreOfCityCateRequest db);
        Task<IEnumerable<StoreDtos>> ListStoreSearch(SearchStoreRequest reqSearch);
        Task<IEnumerable<StoreDtos>> ListStoreByDistrict(StoreMenuBotRequest reqStoreMenuBot);
        Task<ViewLogin> LoginSystemStore(ModelLoginStore modelLogin);
        Task<StoreSystem> UpdateInfoStore(UpdateStoreRequest model);
        Task<StoreSystem> UpdateStoreStatus(int StoreID);
        Task<StoreSystem> SystemInfoStore(int StoreID);
    }
}
