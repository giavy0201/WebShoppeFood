using DAL.Entities;
using DAL.ModelsRequest;
using DAL.Non_Repository.AddressRepo;
using DAL.Non_Repository.MenuRepo;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Unidecode.NET;

namespace DAL.Non_Repository.StoreRepo
{
    public class StoreRepository : IStoreRepository
    {
        private readonly IRepository<Store> _storeRepo;
        private readonly IRepository<AccountStore> _accountStoreRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly IMenuRepository _menuRepo;
        private readonly DataContext _dataContext;

        public StoreRepository(IRepository<Store> storeRepo, IAddressRepository addressRepository, DataContext dataContext, IRepository<AccountStore> accountStoreRepo, IMenuRepository menuRepo)
        {
            _storeRepo = storeRepo;
            _addressRepo = addressRepository;
            _dataContext = dataContext;
            _accountStoreRepo = accountStoreRepo;
            _menuRepo = menuRepo;
        }

        public async Task<Store> GetStoreByID(int StoreID)
        {
            return _storeRepo.GetById(StoreID);
        }

        public async Task<IEnumerable<ViewStore>> ListStoreByCityCate(SearchStoreReq request)
        {
            if (!string.IsNullOrEmpty(request.KeyWord))
            {
                request.KeyWord = request.KeyWord.Unidecode().ToLower();
            }
            var dbStore = _dataContext.Stores
                .Include(x => x.Ward).ThenInclude(x => x.District)
                .Include(x => x.ContentProduct)
                .Where(x => x.Ward.District.CityID == request.CityID
                        && x.ContentProduct.CategoryID == request.CateID
                        && x.Status == ValueGeneric.Active
                && (request.Districts == null || request.Districts.Count() == 0 || request.Districts.Contains(x.Ward.DistrictID))
                && (request.Contents == null || request.Contents.Count() == 0 || request.Contents.Contains(x.ContentID))
                && (request.KeyWord == null || (x.NameNoDiacritic.ToLower().Contains(request.KeyWord)
                                            || x.AddressNoDiacritic.ToLower().Contains(request.KeyWord)
                                            || _menuRepo.GetListStoreByKey(request.KeyWord).ToList().Contains(x.Id)
                                            ))
            );

            var wardIds = dbStore.Select(x => x.WardID).Distinct().ToList();
            var addressLocation = wardIds.ToDictionary(id => id, id => _addressRepo.GetLocationByWard(id).Address);
            var result = await dbStore
                .Select(x => new ViewStore
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Preferential = x.Preferential,
                    Address = x.Address,
                    AddressLocation = addressLocation[x.WardID],
                    DistrictID = x.Ward.DistrictID,
                    ContentID = x.ContentID
                }).ToListAsync();
            if ((request.NumberOfItem != null && request.NumberOfItem != 0) && (request.PageIndex != null && request.PageIndex != 0))
            {
                result = result.Skip(((int)request.PageIndex - 1) * (int)request.NumberOfItem).Take((int)request.NumberOfItem).ToList();
            }
            return result;
        }

        public async Task<IEnumerable<ViewStore>> ListStorePreByCityCate(StoreOfCityCateReq request)
        {
            var dbStore = _dataContext.Stores
                .Include(x => x.Ward).ThenInclude(x => x.District)
                .Include(x => x.ContentProduct)
                .Where(x => x.Ward.District.CityID == request.CityID
                        && x.ContentProduct.CategoryID == request.CateID
                        && !string.IsNullOrEmpty(x.Preferential) && x.Status == ValueGeneric.Active
                        && (request.Districts == null || request.Districts.Count() == 0 || request.Districts.Contains(x.Ward.DistrictID))
                        );

            var wardIds = dbStore.Select(x => x.WardID).Distinct().ToList();
            var addressLocation = wardIds.ToDictionary(id => id, id => _addressRepo.GetLocationByWard(id).Address);

            var result = await dbStore
                .Select(x => new ViewStore
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Preferential = x.Preferential,
                    Address = x.Address,
                    DistrictID = x.Ward.DistrictID,
                    AddressLocation = addressLocation[x.WardID]
                }).ToListAsync();

            if ((request.NumberOfItem != null && request.NumberOfItem != 0) && (request.PageIndex != null && request.PageIndex != 0))
            {
                result = result.Skip(((int)request.PageIndex - 1) * (int)request.NumberOfItem).Take((int)request.NumberOfItem).ToList();
            }
            return result;
        }

        public async Task<IEnumerable<ViewStore>> ListStoreMenuBot(SearchStoreBotReq request)
        {
            var dbStore = _dataContext.Stores
                        .Include(x => x.Ward).ThenInclude(x => x.District)
                        .Include(x => x.ContentProduct)
                        .Where(x => x.Ward.District.CityID == request.CityID
                                && x.ContentProduct.CategoryID == request.CateID
                                && !string.IsNullOrEmpty(x.Preferential) && x.Status == ValueGeneric.Active
                                && (request.DistrictID == null || request.DistrictID == 0 || x.Ward.DistrictID == request.DistrictID)
                                );

            var wardIds = dbStore.Select(x => x.WardID).Distinct().ToList();
            var addressLocation = wardIds.ToDictionary(id => id, id => _addressRepo.GetLocationByWard(id).Address);

            var result = await dbStore
                .Select(x => new ViewStore
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Preferential = x.Preferential,
                    Address = x.Address,
                    DistrictID = x.Ward.DistrictID,
                    AddressLocation = addressLocation[x.WardID]
                }).ToListAsync();

            if ((request.NumberOfItem != null && request.NumberOfItem != 0) && (request.PageIndex != null && request.PageIndex != 0))
            {
                result = result.Skip(((int)request.PageIndex - 1) * (int)request.NumberOfItem).Take((int)request.NumberOfItem).ToList();
            }
            return result;
        }

        public async Task<ViewLoginStore> LoginSystemStore(string UserName, string Password)
        {
            ViewLoginStore viewLogin = new();
            var user = _accountStoreRepo.GetAll().Where(x => x.LoginName == UserName && x.Password == Password).FirstOrDefault();
            if (user == null)
            {
                viewLogin.StatusCode = 0;
                viewLogin.StatusMessage = "Đăng nhập thất bại, kiểm tra lại thông tin";
                return viewLogin;
            }
            else
            {
                viewLogin.StatusCode = 200;
                viewLogin.StatusMessage = "Đăng nhập thành công";
                viewLogin.Username = user.UserName;
                viewLogin.StoreID = user.StoreID;
                return viewLogin;
            }
        }

        public async Task<Store> UpdateInfoStore(UpdateStoreReq updateStoreRequest)
        {
            var store = _storeRepo.GetById(updateStoreRequest.StoreID);
            if (store == null)
            {
                return null;
            }
            store.Name = updateStoreRequest.Name;
            store.NameNoDiacritic = updateStoreRequest.Name.Unidecode();
            store.TimeOpen = TimeSpan.Parse(updateStoreRequest.TimeOpen);
            store.TimeClose = TimeSpan.Parse(updateStoreRequest.TimeClose);
            store.Preferential = updateStoreRequest.Preferential;
            store.Address = updateStoreRequest.Location;
            store.AddressNoDiacritic = updateStoreRequest.Location.Unidecode();
            store.WardID = updateStoreRequest.WardID;
            store.ContentID = updateStoreRequest.ContentID;
            store.AdminName = updateStoreRequest.AdminName;
            store.AdminTime = DateTime.Now;
            store.Status = updateStoreRequest.Status;
            if (updateStoreRequest.Img != null)
            {
                store.Image = updateStoreRequest.Img;
            }
            _storeRepo.Update(store);
            _storeRepo.Save();
            return store;
        }

        public async Task<Store> UpdateStatusStore(int StoreID)
        {
            var store = _storeRepo.GetById(StoreID);
            if (store == null)
            {
                return null;
            }
            if (store.Status == ValueGeneric.Active)
            {
                store.Status = ValueGeneric.OffActive;
            }
            else if (store.Status == ValueGeneric.OffActive)
            {
                store.Status = ValueGeneric.Active;
            }
            _storeRepo.Update(store);
            _storeRepo.Save();
            return store;
        }
    }
}
