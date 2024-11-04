using Azure.Core;
using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;
using DAL.Non_Repository.AddressRepo;
using DAL.Repository;

namespace DAL.Non_Repository.CollectionRepo
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly IRepository<Collection> _CollectionRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly DataContext _dataContext;
        public CollectionRepository(IRepository<Collection> collectionRepo,IAddressRepository addressRepository, DataContext dataContext)
        {
            _CollectionRepo = collectionRepo;
            _addressRepo = addressRepository;
            _dataContext = dataContext;
        }

        public IEnumerable<Collection> ListCollectionByCityCate(ListCollectionByCityCate modelReq)
        {
            var listCollection = _CollectionRepo.GetAll().Where(x => x.CategoryID == modelReq.CateID && x.CityID == modelReq.CityID && x.Status == ValueGeneric.Active).ToList();
            if ((modelReq.NumberOfItem != null && modelReq.NumberOfItem != 0) && (modelReq.PageIndex != null && modelReq.PageIndex != 0))
            {
                listCollection = listCollection.Skip(((int)modelReq.PageIndex - 1) * (int)modelReq.NumberOfItem).Take((int)modelReq.NumberOfItem).ToList();
            }
            return listCollection;
        }
        public Collection GetCollectionById(int CollectionID)
        {
            return _CollectionRepo.GetById(CollectionID);
        }
        public IEnumerable<ViewListStoreOfCollection> ListStoreOfCollection(ListStoreOfCollection modelReq)
        {
            var collectionStore = _dataContext.CollectionStores.Where(x=>x.CollectionID == modelReq.CollectionID && x.Status == ValueGeneric.Active);
            var listStore = _dataContext.Stores;
            var wardIds = listStore.Select(x => x.WardID).Distinct().ToList();
            var addressLocation = wardIds.ToDictionary(id => id, id => _addressRepo.GetLocationByWard(id).Address);
            var result = (from x in collectionStore
                          join y in listStore on x.StoreID equals y.Id
                          select new ViewListStoreOfCollection
                          {
                              Id = x.Id,    
                              CollectionID = modelReq.CollectionID,
                              StoreID = y.Id,
                              StoreImg = y.Image,
                              StoreName = y.Name,
                              StoreAddress = y.Address,
                              StroreLocation = addressLocation[y.WardID],
                              StorePreferential = y.Preferential
                          }).ToList();
            if ((modelReq.NumberOfItem != null && modelReq.NumberOfItem != 0) && (modelReq.PageIndex != null && modelReq.PageIndex != 0))
            {
                result = result.Skip(((int)modelReq.PageIndex - 1) * (int)modelReq.NumberOfItem).Take((int)modelReq.NumberOfItem).ToList();
            }
            return result;
        }
    }
}
