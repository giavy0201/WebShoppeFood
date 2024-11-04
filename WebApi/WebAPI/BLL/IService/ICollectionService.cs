using BLL.Models.DTOs.CollectionDtos;
using BLL.Models.Request;

namespace BLL.IService
{
    public interface ICollectionService
    {
        Task<IEnumerable<CollecDtos>> ListCollectionByCityCate(SearchCollectionHomeRequest reqCollectionHome);
        Task<CollecDtos> GetCollectionById(int CollectionID);
        Task<IEnumerable<ListStoreOfCollectionDtos>> ListStoreByCollection(StoreCollectionRequest reqStoreCollection);
    }
}