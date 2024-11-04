using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;

namespace DAL.Non_Repository.CollectionRepo
{
    public interface ICollectionRepository
    {
        IEnumerable<Collection> ListCollectionByCityCate(ListCollectionByCityCate modelReq);
        Collection GetCollectionById(int CollectionID);
        IEnumerable<ViewListStoreOfCollection> ListStoreOfCollection(ListStoreOfCollection modelReq);
    }
}
