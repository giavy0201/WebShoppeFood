using AutoMapper;
using BLL.IService;
using BLL.Models;
using BLL.Models.DTOs;
using BLL.Models.DTOs.AccountStore;
using BLL.Models.Request;
using BLL.Models.Request.Store;
using DAL.Entities;
using DAL.ModelsRequest;
using DAL.Non_Repository.AddressRepo;
using DAL.Non_Repository.ProductRepo;
using DAL.Non_Repository.StoreRepo;

namespace BLL.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly IProductRepository _productRepo;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;

        public StoreService(IStoreRepository storeRepo, IAddressRepository addressRepo, IProductRepository productRepo, IMapper mapper, ICacheService cacheService)
        {
            _storeRepo = storeRepo;
            _addressRepo = addressRepo;
            _productRepo = productRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        /// <summary>
        /// Select Store by ID
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public async Task<StoreDtos> GetStoreById(int StoreID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.InfoStoreBy, StoreID.ToString());
            //var infoStoreCache = await _cacheService.GetCachedData<Store>(stringKey);
            //if (infoStoreCache == null)
            //{
            //    var getStore = await _storeRepo.GetStoreByID(StoreID);
            //    if (getStore == null) return null;
            //    await _cacheService.SetCachedData(stringKey, getStore, ValueTime.MediumTime);
            //    infoStoreCache = getStore;
            //}
            //var req = _mapper.Map<StoreDtos>(infoStoreCache);
            //var stringKeyLocation = GetKeyCache.CreateKey(GetKeyCache.LocationByWardID, infoStoreCache.WardID.ToString());
            //var locationByWardCache = await _cacheService.GetCachedData<ViewModelLocation>(stringKeyLocation);
            //if (locationByWardCache == null)
            //{
            //    locationByWardCache = _addressRepo.GetLocationByWard(infoStoreCache.WardID);
            //    await _cacheService.SetCachedData(stringKeyLocation, locationByWardCache);
            //}
            //req.AddressLocation = locationByWardCache.Address;
            //return req;

            var getStore = await _storeRepo.GetStoreByID(StoreID);
            if (getStore == null) return null;
            var req = _mapper.Map<StoreDtos>(getStore);

            req.AddressLocation = _addressRepo.GetLocationByWard(getStore.WardID).Address;
            return req;


        }

        /// <summary>
        /// List Store Preferential by City and Category
        /// </summary>
        /// <param name="reqStorePreCityCate"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoreDtos>> ListStorePreByCityAndCate(StoreOfCityCateRequest reqStorePreCityCate)
        {
            var request = _mapper.Map<StoreOfCityCateReq>(reqStorePreCityCate);

            ////list default store preferential search
            //var stringKeySearch = GetKeyCache.CreateKey(GetKeyCache.ListStorePreDefault, reqStorePreCityCate.CityID.ToString(), reqStorePreCityCate.CateID.ToString(), "12", "1");
            //if (reqStorePreCityCate.Districts == null && reqStorePreCityCate.NumberOfItem == 12 && reqStorePreCityCate.PageIndex == 1)
            //{
            //    var listStorePreCache = await _cacheService.GetCachedData<IEnumerable<ViewStore>>(stringKeySearch);
            //    if (listStorePreCache == null)
            //    {
            //        var listStoreDefault = await _storeRepo.ListStorePreByCityCate(request);
            //        await _cacheService.SetCachedData(stringKeySearch, listStoreDefault, ValueTime.MediumTime);
            //        listStorePreCache = listStoreDefault;
            //    }
            //    return _mapper.Map<IEnumerable<StoreDtos>>(listStorePreCache);
            //}

            ////list default store preferential home
            //var stringKeyHome = GetKeyCache.CreateKey(GetKeyCache.ListStorePreDefault, reqStorePreCityCate.CityID.ToString(), reqStorePreCityCate.CateID.ToString(), "6", "1");
            //if (reqStorePreCityCate.Districts == null && reqStorePreCityCate.NumberOfItem == 6 && reqStorePreCityCate.PageIndex == 1)
            //{
            //    var listStorePreHomeCache = await _cacheService.GetCachedData<IEnumerable<ViewStore>>(stringKeyHome);
            //    if (listStorePreHomeCache == null)
            //    {
            //        var listStoreDefault = await _storeRepo.ListStorePreByCityCate(request);
            //        await _cacheService.SetCachedData(stringKeyHome, listStoreDefault, ValueTime.MediumTime);
            //        listStorePreHomeCache = listStoreDefault;
            //    }
            //    return _mapper.Map<IEnumerable<StoreDtos>>(listStorePreHomeCache);
            //}

            // has options
            var listStore = await _storeRepo.ListStorePreByCityCate(request);
            var listStoreAll = _mapper.Map<IEnumerable<StoreDtos>>(listStore);
            return listStoreAll;
        }

        /// <summary>
        /// Api Search
        /// </summary>
        /// <param name="reqSearch"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoreDtos>> ListStoreSearch(SearchStoreRequest reqSearch)
        {
            var request = _mapper.Map<SearchStoreReq>(reqSearch);
            //var test = new SearchStoreRequest { CateID = reqSearch.CateID };    

            //// list default store search
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListStoreSearchDefault, reqSearch.CityID.ToString(), reqSearch.CateID.ToString(), "12", "1");
            //if (string.IsNullOrEmpty(reqSearch.KeyWord) && reqSearch.Districts == null && reqSearch.Contents == null && reqSearch.NumberOfItem == 12 && reqSearch.PageIndex == 1)
            //{
            //    var listStoreSearchCache = await _cacheService.GetCachedData<IEnumerable<ViewStore>>(stringKey);
            //    if (listStoreSearchCache == null)
            //    {
            //        var listStore = await _storeRepo.ListStoreByCityCate(request);
            //        await _cacheService.SetCachedData(stringKey, listStore, ValueTime.MediumTime);
            //        listStoreSearchCache = listStore;
            //    }
            //    return _mapper.Map<IEnumerable<StoreDtos>>(listStoreSearchCache);
            //}

            //has options
            var listStoreReq = await _storeRepo.ListStoreByCityCate(request);
            return _mapper.Map<IEnumerable<StoreDtos>>(listStoreReq);
        }

        /// <summary>
        /// List Store By District ID
        /// </summary>
        /// <param name="reqStoreMenuBot"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoreDtos>> ListStoreByDistrict(StoreMenuBotRequest reqStoreMenuBot)
        {
            var request = _mapper.Map<SearchStoreBotReq>(reqStoreMenuBot);
            ////list store menu bot default
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListStoreDistrictDefault, reqStoreMenuBot.CityID.ToString(), reqStoreMenuBot.CateID.ToString(), "6", "1");
            //if ((reqStoreMenuBot.DistrictID == null || reqStoreMenuBot.DistrictID == 0) && reqStoreMenuBot.NumberOfItem == 6 && reqStoreMenuBot.PageIndex == 1)
            //{
            //    var listDistrictDefault = await _cacheService.GetCachedData<IEnumerable<ViewStore>>(stringKey);
            //    if (listDistrictDefault == null)
            //    {
            //        var listStoreDefault = await _storeRepo.ListStoreMenuBot(request);
            //        await _cacheService.SetCachedData(stringKey, listStoreDefault, ValueTime.MediumTime);
            //        listDistrictDefault = listStoreDefault;
            //    }
            //    return _mapper.Map<IEnumerable<StoreDtos>>(listDistrictDefault);
            //}

            //has options
            var listStoreBot = await _storeRepo.ListStoreMenuBot(request);
            var listStoreDtos = _mapper.Map<IEnumerable<StoreDtos>>(listStoreBot);
            return listStoreDtos;
        }

        public async Task<ViewLogin> LoginSystemStore(ModelLoginStore modelLogin)
        {
            var viewlogin = await _storeRepo.LoginSystemStore(modelLogin.LoginName, modelLogin.Password);
            return _mapper.Map<ViewLogin>(viewlogin);
        }

        public async Task<StoreSystem> UpdateInfoStore(UpdateStoreRequest model)
        {
            var WardID = _addressRepo.GetWardByID((int)model.WardID);
            var ContentID = _productRepo.GetContentProductByID((int)model.ContentID);
            var req = _mapper.Map<UpdateStoreReq>(model);
            var store = await _storeRepo.UpdateInfoStore(req);
            //await RemoveCache(model.StoreID, WardID.District.CityID, ContentID.CategoryID);
            return _mapper.Map<StoreSystem>(store);
        }

        public async Task<StoreSystem> UpdateStoreStatus(int StoreID)
        {
            var store = await _storeRepo.UpdateStatusStore(StoreID);
            var ContentID = _productRepo.GetContentProductByID(store.ContentID);
            var WardID = _addressRepo.GetWardByID(store.WardID);
            //await RemoveCache(StoreID, WardID.District.CityID, ContentID.CategoryID);
            return _mapper.Map<StoreSystem>(store);
        }

        public async Task<StoreSystem> SystemInfoStore(int StoreID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.InfoStoreBy, StoreID.ToString());
            //var infoStoreCache = await _cacheService.GetCachedData<Store>(stringKey);
            //if (infoStoreCache == null)
            //{
            //    var getStore = await _storeRepo.GetStoreByID(StoreID);
            //    if (getStore == null) return null;
            //    await _cacheService.SetCachedData(stringKey, getStore, ValueTime.MediumTime);
            //    infoStoreCache = getStore;
            //}
            //var request = _mapper.Map<StoreSystem>(infoStoreCache);
            //var stringKeyLocation = GetKeyCache.CreateKey(GetKeyCache.LocationByWardID, infoStoreCache.WardID.ToString());
            //var locationByWardCache = await _cacheService.GetCachedData<ViewModelLocation>(stringKeyLocation);
            //if (locationByWardCache == null)
            //{
            //    locationByWardCache = _addressRepo.GetLocationByWard(infoStoreCache.WardID);
            //    await _cacheService.SetCachedData(stringKeyLocation, locationByWardCache);
            //}
            //request.AddressLocation = locationByWardCache.Address;
            //return request;

            var getStore = await _storeRepo.GetStoreByID(StoreID);
            if (getStore == null) return null;
            var request = _mapper.Map<StoreSystem>(getStore);
            request.AddressLocation = _addressRepo.GetLocationByWard(getStore.WardID).Address;
            return request;



        }
        #region Private Methods
        private async Task RemoveCache(int StoreID, int CityID, int CategoryID)
        {
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.InfoStoreBy, StoreID.ToString()));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListStorePreDefault, CityID.ToString(), CategoryID.ToString(), "12", "1"));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListStoreSearchDefault, CityID.ToString(), CategoryID.ToString(), "12", "1"));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListStorePreDefault, CityID.ToString(), CategoryID.ToString(), "6", "1"));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListStoreDistrictDefault, CityID.ToString(), CategoryID.ToString(), "6", "1"));
        }
        #endregion Private Methods
    }
}
