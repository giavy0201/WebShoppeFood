using AutoMapper;
using BLL.IService;
using BLL.Models;
using BLL.Models.DTOs.CollectionDtos;
using BLL.Models.Request;
using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;
using DAL.Non_Repository.CollectionRepo;

namespace BLL.Service
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepo;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public CollectionService(ICollectionRepository collectionRepo, IMapper mapper, ICacheService cacheService)
        {
            _collectionRepo = collectionRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        /// <summary>
        /// Select Collection by Id
        /// </summary>
        /// <param name="CollectionID"></param>
        /// <returns></returns>
        public async Task<CollecDtos> GetCollectionById(int CollectionID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.SelectCollection, CollectionID.ToString());
            //var collectionCache = await _cacheService.GetCachedData<Collection>(stringKey);
            //if (collectionCache == null)
            //{
            //    var collection = _collectionRepo.GetCollectionById(CollectionID);
            //    await _cacheService.SetCachedData(stringKey, collection, ValueTime.MediumTime);
            //    collectionCache = collection;
            //}
            //return _mapper.Map<CollecDtos>(collectionCache);

            var collection = _collectionRepo.GetCollectionById(CollectionID);
            return _mapper.Map<CollecDtos>(collection);
        }

        /// <summary>
        /// select list collection 
        /// </summary>
        /// <param name="reqCollectionHome"></param>
        /// <returns></returns>
        public async Task<IEnumerable<CollecDtos>> ListCollectionByCityCate(SearchCollectionHomeRequest reqCollectionHome)
        {
            var req = _mapper.Map<ListCollectionByCityCate>(reqCollectionHome);
            ////list collection search default
            //var stringKeySearch = GetKeyCache.CreateKey(GetKeyCache.CollectionDefault, reqCollectionHome.CityID.ToString(), reqCollectionHome.CateID.ToString(), "12", "1");
            //if (reqCollectionHome.NumberOfItem == 12 && reqCollectionHome.PageIndex == 1)
            //{
            //    var listCollectionCache = await _cacheService.GetCachedData<IEnumerable<Collection>>(stringKeySearch);
            //    if (listCollectionCache == null)
            //    {
            //        var listCollectionSearch = _collectionRepo.ListCollectionByCityCate(req);
            //        await _cacheService.SetCachedData(stringKeySearch, listCollectionSearch, ValueTime.MediumTime);
            //        listCollectionCache = listCollectionSearch;
            //    }
            //    return _mapper.Map<List<CollecDtos>>(listCollectionCache);
            //}

            ////list collection home default
            //var stringKeyHome = GetKeyCache.CreateKey(GetKeyCache.CollectionDefault, reqCollectionHome.CityID.ToString(), reqCollectionHome.CateID.ToString(), "6", "1");
            //if (reqCollectionHome.NumberOfItem == 6 && reqCollectionHome.PageIndex == 1)
            //{
            //    var listCollectionCache = await _cacheService.GetCachedData<IEnumerable<Collection>>(stringKeyHome);
            //    if (listCollectionCache == null)
            //    {
            //        var listCollectionSearch = _collectionRepo.ListCollectionByCityCate(req);
            //        await _cacheService.SetCachedData(stringKeyHome, listCollectionSearch, ValueTime.MediumTime);
            //        listCollectionCache = listCollectionSearch;
            //    }
            //    return _mapper.Map<List<CollecDtos>>(listCollectionCache);
            //}
            //has paging
            var listCollectionByCate = _collectionRepo.ListCollectionByCityCate(req);
            var result = _mapper.Map<List<CollecDtos>>(listCollectionByCate);
            return result;
        }

        /// <summary>
        /// list store of collection
        /// </summary>
        /// <param name="reqStoreCollection"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ListStoreOfCollectionDtos>> ListStoreByCollection(StoreCollectionRequest reqStoreCollection)
        {
            var request = _mapper.Map<ListStoreOfCollection>(reqStoreCollection);
            ////list store of collection default
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListStoreOfCollectionDefault, reqStoreCollection.CollectionID.ToString(), "12", "1");
            //if (reqStoreCollection.NumberOfItem == 12 && reqStoreCollection.PageIndex == 1)
            //{
            //    var listStoreOfCollectionCache = await _cacheService.GetCachedData<IEnumerable<ViewListStoreOfCollection>>(stringKey);
            //    if (listStoreOfCollectionCache == null)
            //    {
            //        var listStoreCache = _collectionRepo.ListStoreOfCollection(request);
            //        await _cacheService.SetCachedData(stringKey, listStoreCache, ValueTime.MediumTime);
            //        listStoreOfCollectionCache = listStoreCache;
            //    }
            //    return _mapper.Map<List<ListStoreOfCollectionDtos>>(listStoreOfCollectionCache);
            //}
            //has paging
            var listStore = _collectionRepo.ListStoreOfCollection(request);
            var result = _mapper.Map<List<ListStoreOfCollectionDtos>>(listStore);
            return result;
        }
    }
}
