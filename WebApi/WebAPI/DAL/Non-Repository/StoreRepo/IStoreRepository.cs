using DAL.Entities;
using DAL.ModelsRequest;

namespace DAL.Non_Repository.StoreRepo
{
    public interface IStoreRepository
    {
        Task<Store> GetStoreByID(int StoreID);
        Task<IEnumerable<ViewStore>> ListStoreByCityCate(SearchStoreReq request);
        Task<IEnumerable<ViewStore>> ListStorePreByCityCate(StoreOfCityCateReq request);
        Task<IEnumerable<ViewStore>> ListStoreMenuBot(SearchStoreBotReq request);
        Task<ViewLoginStore> LoginSystemStore(string UserName, string Password);
        Task<Store> UpdateInfoStore(UpdateStoreReq updateStoreRequest);
        Task<Store> UpdateStatusStore(int StoreID);
    }
}
