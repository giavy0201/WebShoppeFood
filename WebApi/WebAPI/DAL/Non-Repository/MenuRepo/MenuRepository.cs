using DAL.Entities;
using DAL.ModelsRequest;
using DAL.ModelsRequest.MenuRequest;
using DAL.Repository;
using Unidecode.NET;
using static DAL.Generic.SortByColumn;

namespace DAL.Non_Repository.MenuRepo
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IRepository<ListMenu> _menuRepo;
        private readonly IRepository<Food> _foodRepo;
        private readonly DataContext _dataContext;

        public MenuRepository(IRepository<ListMenu> menuRepo, IRepository<Food> foodRepo, DataContext dataContext)
        {
            _menuRepo = menuRepo;
            _foodRepo = foodRepo;
            _dataContext = dataContext;
        }

        public IEnumerable<Food> ListFoodOfMenu(int MenuID)
        {
            return _foodRepo.GetAll().Where(x => x.MenuID == MenuID && x.Status == ValueGeneric.Active).ToList();
        }

        public IEnumerable<ListMenu> ListMenuOfStore(int StoreID)
        {
            var dbMenu = _dataContext.ListMenus
                        .Where(x => x.StoreID == StoreID && x.Status == ValueGeneric.Active)
                        .ToList();
            return dbMenu;
        }

        public IEnumerable<ViewMenuFoodOfStore> ListMenuByStore(int StoreID)
        {
            var store = _dataContext.Stores.Where(x => x.Id == StoreID);
            var menu = _dataContext.ListMenus.Where(x => x.Status == ValueGeneric.Active);
            var food = _dataContext.Foods.Where(x => x.Status == ValueGeneric.Active);
            var result = (from x in food
                          join y in menu on x.MenuID equals y.Id
                          join z in store on y.StoreID equals z.Id
                          select new ViewMenuFoodOfStore
                          {
                              StoreID = StoreID,
                              StoreName = z.Name,
                              MenuName = y.Name,
                              FoodName = x.Name,
                              Image = x.Image,
                              Description = x.Description,
                              Price = x.Price,
                              Discount = x.Discount
                          }).ToList();
            return result;
        }

        public IEnumerable<int> GetListStoreByKey(string keyword)
        {
            var listFoodIdByKey = _foodRepo.GetAll().Where(x => x.Name.Unidecode().ToLower().Contains(keyword.Unidecode().ToLower()) && x.Status == ValueGeneric.Active)
                                         .Select(y => y.MenuID).ToList();
            var listMenuID = _menuRepo.GetAll().Where(x => listFoodIdByKey.Contains(x.Id)).Select(y => y.StoreID).ToList();
            return listMenuID;
        }

        public ViewFoodDtos DeatailFood(int FoodID)
        {
            var store = _dataContext.Stores;
            var menu = _dataContext.ListMenus;
            var food = _dataContext.Foods.Where(x => x.Id == FoodID);
            var result = (from x in food
                          join y in menu on x.MenuID equals y.Id
                          join z in store on y.StoreID equals z.Id
                          select new ViewFoodDtos
                          {
                              Id = FoodID,
                              Name = x.Name,
                              Image = x.Image,
                              Price = x.Price,
                              Discount = x.Discount,
                              Description = x.Description,
                              StoreID = y.StoreID
                          }).FirstOrDefault();
            return result;
        }

        public async Task<List<ListMenu>> GetListMenuByStore(int StoreID)
        {
            var dbMenu = _dataContext.ListMenus
                        .Where(x => x.StoreID == StoreID && (x.Status == ValueGeneric.Active || x.Status == ValueGeneric.OffActive))
                        .ToList();
            return dbMenu;
        }

        public async Task<ListMenu> AddMenu(AddMenuReq addMenuReq)
        {
            var menu = new ListMenu
            {
                Name = addMenuReq.MenuName,
                StoreID = addMenuReq.StoreID,
                AdminName = addMenuReq.AdminName,
                AdminTime = DateTime.Now,
                Status = addMenuReq.Status
            };
            _menuRepo.Insert(menu);
            _menuRepo.Save();
            return menu;
        }

        public async Task<ListMenu> UpdateMenu(UpdateMenuReq updateMenuReq)
        {
            var menu = _menuRepo.GetById(updateMenuReq.MenuID);
            if (menu == null)
            {
                return null;
            }
            menu.Name = updateMenuReq.MenuName;
            menu.AdminName = updateMenuReq.AdminName;
            menu.AdminTime = DateTime.Now;
            menu.Status = updateMenuReq.Status;
            _menuRepo.Update(menu);
            _menuRepo.Save();
            return menu;
        }

        public async Task<ListMenu> DetailMenu(int MenuID)
        {
            var menu = _menuRepo.GetById(MenuID);
            if (menu == null)
            {
                return null;
            }
            return menu;
        }

        public async Task<ListMenu> DeleteMenu(int MenuID)
        {
            var menu = _menuRepo.GetById(MenuID);
            if (menu == null)
            {
                return null;
            }
            menu.Status = ValueGeneric.Delete;
            _menuRepo.Update(menu);
            _menuRepo.Save();
            return menu;
        }

        public async Task<ListMenu> UpdateStatusMenu(int MenuID)
        {
            var menu = _menuRepo.GetById(MenuID);
            if (menu == null)
            {
                return null;
            }
            if (menu.Status == ValueGeneric.Active)
            {
                menu.Status = ValueGeneric.OffActive;
            }
            else if (menu.Status == ValueGeneric.OffActive)
            {
                menu.Status = ValueGeneric.Active;
            }
            _menuRepo.Update(menu);
            _menuRepo.Save();
            return menu;
        }

        public async Task<List<Food>> GetListFoodByMenu(int MenuID)
        {
            var listFood = _foodRepo.GetAll().Where(x => x.MenuID == MenuID && (x.Status == ValueGeneric.Active || x.Status == ValueGeneric.OffActive)).ToList();
            return listFood;
        }

        public async Task<Food> AddFood(AddFoodReq addFoodReq)
        {
            var food = new Food
            {
                Name = addFoodReq.Name,
                NameNoDiacritic = addFoodReq.Name.Unidecode(),
                Image = addFoodReq.Img,
                Price = addFoodReq.Price,
                Discount = addFoodReq.Discount,
                Description = addFoodReq.Description,
                MenuID = addFoodReq.MenuID,
                AdminName = addFoodReq.AdminName,
                AdminTime = DateTime.Now,
                Status = addFoodReq.Status
            };
            _foodRepo.Insert(food);
            _foodRepo.Save();
            return food;
        }

        public async Task<Food> UpdateFood(UpdateFoodReq updateFoodReq)
        {
            var food = _foodRepo.GetById(updateFoodReq.FoodID);
            if (food == null)
            {
                return null;
            }
            food.Name = updateFoodReq.Name;
            food.NameNoDiacritic = updateFoodReq.Name.Unidecode();
            food.Price = updateFoodReq.Price;
            food.Discount = updateFoodReq.Discount;
            food.Description = updateFoodReq.Description;
            food.MenuID = updateFoodReq.MenuID;
            food.AdminName = updateFoodReq.AdminName;
            food.AdminTime = DateTime.Now;
            food.Status = updateFoodReq.Status;
            if (updateFoodReq.Img != null)
            {
                food.Image = updateFoodReq.Img;
            }
            _foodRepo.Update(food);
            _foodRepo.Save();
            return food;
        }

        public async Task<Food> DetailFoodAdmin(int FoodID)
        {
            var food = _foodRepo.GetById(FoodID);
            if (food == null)
            {
                return null;
            }
            return food;
        }

        public async Task<Food> DeleteFood(int FoodID)
        {
            var food = _foodRepo.GetById(FoodID);
            if (food == null)
            {
                return null;
            }
            food.Status = ValueGeneric.Delete;
            _foodRepo.Update(food);
            _foodRepo.Save();
            return food;
        }

        public async Task<Food> UpdateStatusFood(int FoodID)
        {
            var food = _foodRepo.GetById(FoodID);
            if (food == null)
            {
                return null;
            }
            if (food.Status == ValueGeneric.Active)
            {
                food.Status = ValueGeneric.OffActive;
            }
            else if (food.Status == ValueGeneric.OffActive)
            {
                food.Status = ValueGeneric.Active;
            }
            _foodRepo.Update(food);
            _foodRepo.Save();
            return food;
        }

        public async Task<List<Food>> GetListFoodByStore(int StoreID)
        {
            var store = _dataContext.Stores.Where(x => x.Id == StoreID);
            var menu = _dataContext.ListMenus.Where(x => (x.Status == ValueGeneric.Active || x.Status == ValueGeneric.OffActive));
            var food = _dataContext.Foods.Where(x => (x.Status == ValueGeneric.Active || x.Status == ValueGeneric.OffActive));
            var result = (from x in food
                          join y in menu on x.MenuID equals y.Id
                          join z in store on y.StoreID equals z.Id
                          select x).ToList();
            return result;
        }

        public async Task<List<Food>> SearchProduct(SearchProductReq request)
        {
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                request.Keyword = request.Keyword.Unidecode().ToLower();
            }
            var store = _dataContext.Stores.Where(x => x.Id == request.StoreID);
            var menu = _dataContext.ListMenus
                .Where(x => (x.Status != ValueGeneric.Delete)
                && (request.Menu == null || request.Menu.Count == 0 || request.Menu.Contains(x.Id))
            );
            var food = _dataContext.Foods
                .Where(x => ((request.StatusID == null || request.StatusID == x.Status) && x.Status != ValueGeneric.Delete)
                && (request.Keyword == null || x.NameNoDiacritic.ToLower().Contains(request.Keyword))
                && (request.PriceFirst == null || request.PriceFirst == 0 || x.Price >= request.PriceFirst)
                && (request.PriceEnd == null || request.PriceEnd == 0 || x.Price <= request.PriceEnd)
                && (request.DiscountPrice == null || (request.DiscountPrice == ValueDiscountDAL.Discount && x.Discount != 0) || (request.DiscountPrice == ValueDiscountDAL.NotDiscount && x.Discount == 0))
                );
            if (request.SortPrice != null)
            {
                food = Sort(food, "Price", (bool)request.SortPrice);
            }

            var result = (from x in food
                          join y in menu on x.MenuID equals y.Id
                          join z in store on y.StoreID equals z.Id
                          select x).ToList();

            return result;
        }
    }
}
