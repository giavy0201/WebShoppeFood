using AutoMapper;
using BLL.IService;
using BLL.Models;
using BLL.Models.DTOs;
using BLL.Models.DTOs.Food;
using BLL.Models.DTOs.ListMenu;
using BLL.Models.Request;
using BLL.Models.Request.Food;
using BLL.Models.Request.ListMenu;
using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;
using DAL.Non_Repository;
using DAL.Non_Repository.MenuRepo;
using static BLL.Models.Validate.EnumNumber;

namespace BLL.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _menuRepo;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public MenuService(IMenuRepository menuRepo, IMapper mapper, ICacheService cacheService)
        {
            _menuRepo = menuRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        /// <summary>
        /// list menu of store
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ListMenuDtos>> ListMenuOfStore(int StoreID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListMenuByStore, StoreID.ToString());
            //var listMenuByStoreCache = await _cacheService.GetCachedData<List<ListMenu>>(stringKey);
            //if (listMenuByStoreCache == null)
            //{
            //    var listMenu = await _menuRepo.GetListMenuByStore(StoreID);
            //    await _cacheService.SetCachedData(stringKey, listMenu, ValueTime.MediumTime);
            //    var request = listMenu.Where(x => x.Status == ValueGeneric.Active);
            //    return _mapper.Map<List<ListMenuDtos>>(request);
            //}
            //var List = listMenuByStoreCache.Where(x => x.Status == ValueGeneric.Active);
            //return _mapper.Map<List<ListMenuDtos>>(List);

            var listMenu = await _menuRepo.GetListMenuByStore(StoreID);
            var List = listMenu.Where(x => x.Status == ValueGeneric.Active);
            return _mapper.Map<List<ListMenuDtos>>(List);
        }

        /// <summary>
        /// list menu food of store
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ListMenuFoodOfStoreDtos>> ListMenuFoodOfStore(int StoreID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListFoodByStore, StoreID.ToString());
            //var listFoodByStoreCache = await _cacheService.GetCachedData<List<Food>>(stringKey);
            //if (listFoodByStoreCache == null)
            //{
            //    var listfood = await _menuRepo.GetListFoodByStore(StoreID);
            //    await _cacheService.SetCachedData(stringKey, listfood, ValueTime.MediumTime);
            //    var request = listfood.Where(x => x.Status == ValueGeneric.Active);
            //    return _mapper.Map<List<ListMenuFoodOfStoreDtos>>(request);
            //}
            //var list = listFoodByStoreCache.Where(x => x.Status == ValueGeneric.Active);
            //return _mapper.Map<List<ListMenuFoodOfStoreDtos>>(list);


            var listfood = await _menuRepo.GetListFoodByStore(StoreID);
            var list = listfood.Where(x => x.Status == ValueGeneric.Active);
            return _mapper.Map<List<ListMenuFoodOfStoreDtos>>(list);
        }

        public async Task<IEnumerable<FoodDtos>> ListFoodOfMenu(int MenuID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListFoodByMenu, MenuID.ToString());
            //var listFoodByMenuCache = await _cacheService.GetCachedData<List<Food>>(stringKey);
            //if (listFoodByMenuCache == null)
            //{
            //    var listFood = await _menuRepo.GetListFoodByMenu(MenuID);
            //    await _cacheService.SetCachedData(stringKey, listFood, ValueTime.MediumTime);
            //    var request = listFood.Where(x => x.Status == ValueGeneric.Active);
            //    return _mapper.Map<List<FoodDtos>>(request);
            //}
            //var List = listFoodByMenuCache.Where(x => x.Status == ValueGeneric.Active);
            //return _mapper.Map<List<FoodDtos>>(List);

            var listFood = await _menuRepo.GetListFoodByMenu(MenuID);
            var List = listFood.Where(x => x.Status == ValueGeneric.Active);
            return _mapper.Map<List<FoodDtos>>(List);
        }

        public async Task<FoodDtos> DeatailFood(int FoodID)
        {
            var food = _menuRepo.DeatailFood(FoodID);
            return _mapper.Map<FoodDtos>(food);
        }

        public async Task<List<ListMenuSystem>> GetListMenuByStore(int StoreID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListMenuByStore, StoreID.ToString());
            //var listMenuByStoreCache = await _cacheService.GetCachedData<List<ListMenu>>(stringKey);
            //if (listMenuByStoreCache == null)
            //{
            //    var listMenu = await _menuRepo.GetListMenuByStore(StoreID);
            //    await _cacheService.SetCachedData(stringKey, listMenu, ValueTime.MediumTime);
            //    return _mapper.Map<List<ListMenuSystem>>(listMenu);
            //}
            //return _mapper.Map<List<ListMenuSystem>>(listMenuByStoreCache);

            var listMenu = await _menuRepo.GetListMenuByStore(StoreID);
            return _mapper.Map<List<ListMenuSystem>>(listMenu);
        }

        public async Task<ModelApiRequest> AddMenu(AddMenuRequest model)
        {
            int StoreID = (int)model.StoreID;
            var req = new ModelApiRequest();
            var request = _mapper.Map<AddMenuReq>(model);
            var menu = await _menuRepo.AddMenu(request);
            if (menu == null)
            {
                return null;
            }
            //await RemoveCacheMenu(StoreID);
            req.ID = menu.Id;
            return req;
        }

        public async Task<ModelApiRequest> UpdateMenu(UpdateMenuRequest model)
        {
            var selectMenu = await _menuRepo.DetailMenu((int)model.MenuID);
            var req = new ModelApiRequest();
            var updateMenuRequest = _mapper.Map<UpdateMenuReq>(model);
            var menu = await _menuRepo.UpdateMenu(updateMenuRequest);
            if (menu == null)
            {
                return null;
            }
            //await RemoveCacheMenu(selectMenu.StoreID);
            req.ID = menu.Id;
            return req;
        }

        public async Task<ListMenuSystem> DetailMenu(int MenuID)
        {
            var req = await _menuRepo.DetailMenu(MenuID);
            var listMenuSystem = _mapper.Map<ListMenuSystem>(req);
            return listMenuSystem;
        }

        public async Task<ListMenu> DeleteMenu(int MenuID)
        {
            var selectMenu = await _menuRepo.DetailMenu(MenuID);
            var menu = await _menuRepo.DeleteMenu(MenuID);
            //await RemoveCacheMenu(selectMenu.StoreID);
            return menu;
        }

        public async Task<ModelApiRequest> UpdateMenuStatus(int MenuID)
        {
            var selectMenu = await _menuRepo.DetailMenu(MenuID);
            var req = new ModelApiRequest();
            var menu = await _menuRepo.UpdateStatusMenu(MenuID);
            if (menu == null)
            {
                return null;
            }
            //await RemoveCacheMenu(selectMenu.StoreID);
            req.ID = menu.Id;
            return req;
        }

        public async Task<FoodSystem> DetailFoodAdmin(int FoodID)
        {
            var food = await _menuRepo.DetailFoodAdmin(FoodID);
            var foodSystem = _mapper.Map<FoodSystem>(food);
            return foodSystem;
        }

        public async Task<List<FoodSystem>> GetListFoodByMenu(int MenuID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListFoodByMenu, MenuID.ToString());
            //var listFoodByMenuCache = await _cacheService.GetCachedData<List<Food>>(stringKey);
            //if (listFoodByMenuCache == null)
            //{
            //    var listFood = await _menuRepo.GetListFoodByMenu(MenuID);
            //    await _cacheService.SetCachedData(stringKey, listFood, ValueTime.MediumTime);
            //    return _mapper.Map<List<FoodSystem>>(listFood);
            //}
            //return _mapper.Map<List<FoodSystem>>(listFoodByMenuCache);

            var listFood = await _menuRepo.GetListFoodByMenu(MenuID);
            return _mapper.Map<List<FoodSystem>>(listFood);
        }

        public async Task<ModelApiRequest> AddFood(AddFoodRequest model)
        {
            int MenuID = (int)model.MenuID;
            var menu = await _menuRepo.DetailMenu(MenuID);
            var req = new ModelApiRequest();
            var addFoodRequest = _mapper.Map<AddFoodReq>(model);
            var food = await _menuRepo.AddFood(addFoodRequest);
            if (food == null)
            {
                return null;
            }
            //await RemoveCacheFood(MenuID, menu.StoreID);
            req.ID = food.Id;
            return req;
        }

        public async Task<ModelApiRequest> UpdateFood(UpdateFoodRequest model)
        {
            int MenuID = (int)model.MenuID;
            var menu = await _menuRepo.DetailMenu(MenuID);
            var req = new ModelApiRequest();
            var updateFoodReq = _mapper.Map<UpdateFoodReq>(model);
            var food = await _menuRepo.UpdateFood(updateFoodReq);
            if (food == null)
            {
                return null;
            }
            //await RemoveCacheFood(MenuID, menu.StoreID);
            req.ID = food.Id;
            return req;
        }

        public async Task<Food> DeleteFood(int FoodID)
        {
            var selectFood = await _menuRepo.DetailFoodAdmin(FoodID);
            var food = await _menuRepo.DeleteFood(FoodID);
            var selectMenu = await _menuRepo.DetailMenu(selectFood.MenuID);
            //await RemoveCacheFood(selectFood.MenuID, selectMenu.StoreID);
            return food;
        }

        public async Task<ModelApiRequest> UpdateFoodStatus(int FoodID)
        {
            var selectFood = await _menuRepo.DetailFoodAdmin(FoodID);
            var req = new ModelApiRequest();
            var food = await _menuRepo.UpdateStatusFood(FoodID);
            if (food == null)
            {
                return null;
            }
            var selectMenu = await _menuRepo.DetailMenu(selectFood.MenuID);
            //await RemoveCacheFood(selectFood.MenuID, selectMenu.StoreID);
            req.ID = food.Id;
            return req;
        }

        public async Task<List<FoodSystem>> GetListFoodByStore(int StoreID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListFoodByStore, StoreID.ToString());
            //var listFoodByStoreCache = await _cacheService.GetCachedData<List<Food>>(stringKey);
            //if (listFoodByStoreCache == null)
            //{
            //    var listfood = await _menuRepo.GetListFoodByStore(StoreID);
            //    await _cacheService.SetCachedData(stringKey, listfood, ValueTime.MediumTime);
            //    return _mapper.Map<List<FoodSystem>>(listfood);
            //}
            //return _mapper.Map<List<FoodSystem>>(listFoodByStoreCache);

            var listfood = await _menuRepo.GetListFoodByStore(StoreID);
            return _mapper.Map<List<FoodSystem>>(listfood);
        }

        public async Task<List<FoodSystem>> SearchProduct(SelectProductRequest reqProduct)
        {
            if (reqProduct.StatusID != null && (!Enum.IsDefined(typeof(StatusID), reqProduct.StatusID)))
            {
                return null;
            }
            var request = _mapper.Map<SearchProductReq>(reqProduct);
            //list product default
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListProductByStore, reqProduct.StoreID.ToString());
            //if (string.IsNullOrEmpty(reqProduct.Keyword) && (reqProduct.Menu == null || reqProduct.Menu.Count() == 0) && (reqProduct.PriceFirst == null || reqProduct.PriceFirst == 0)
            //    && (reqProduct.PriceEnd == null || reqProduct.PriceEnd == 0) && reqProduct.DiscountPrice == null && reqProduct.SortPrice == null && reqProduct.StatusID == null)
            //{
            //    var listProductByStore = await _cacheService.GetCachedData<List<Food>>(stringKey);
            //    if (listProductByStore == null)
            //    {
            //        var listAll = await _menuRepo.SearchProduct(request);
            //        await _cacheService.SetCachedData(stringKey, listAll, ValueTime.MediumTime);
            //        listProductByStore = listAll;
            //    }
            //    var listRequest = _mapper.Map<List<FoodSystem>>(listProductByStore);
            //    return listRequest;
            //}

            //has options
            var listProduct = await _menuRepo.SearchProduct(request);
            var listProductSystem = _mapper.Map<List<FoodSystem>>(listProduct);
            return listProductSystem;
        }

        #region Private Methods
        private async Task RemoveCacheFood(int MenuID, int StoreID)
        {
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListFoodByMenu, MenuID.ToString()));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListFoodByStore, StoreID.ToString()));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListProductByStore, StoreID.ToString()));
        }

        private async Task RemoveCacheMenu(int StoreID)
        {
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListMenuByStore, StoreID.ToString()));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListFoodByStore, StoreID.ToString()));
            await _cacheService.RemoveCache(GetKeyCache.CreateKey(GetKeyCache.ListProductByStore, StoreID.ToString()));
        }
        #endregion Private Methods
    }
}
