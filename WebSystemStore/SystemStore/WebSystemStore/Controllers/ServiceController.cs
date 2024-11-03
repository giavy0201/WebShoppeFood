using BLL.IService;
using BLL.Model.Cart;
using Microsoft.AspNetCore.Mvc;

namespace WebSystemStore.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStoreService _storeService;
        public ServiceController(IHttpContextAccessor context, IStoreService storeService)
        {
            _context = context;
            _storeService = storeService;
        }
        public async Task<int> GetListCartOrderToday(ModelSelectCart req)
        {
            req.StatusID = 0;
            var listcart = await _storeService.ListCartOrderTodayByStore(req);
            if (listcart.IsSuccess == false)
            {
                var listnow = 0;
                return listnow;
            }
            else
            {
                var listnow = listcart.Data.Select(x => x.Id).Count();
                return listnow;
            }
        }

        public async Task<int> GetListCartToday(ModelSelectCart req)
        {
            var listcart = await _storeService.ListCartOrderTodayByStore(req);
            if (listcart.IsSuccess == false)
            {
                var listnow = 0;
                return listnow;
            }
            else
            {
                var listnow = listcart.Data.Select(x => x.Id).Count();
                return listnow;
            }
        }

        public async Task<double> GetTotalMoneyToDay(int StoreID)
        {
            var totalmoney = await _storeService.TotalMoneyToday(StoreID);
            return totalmoney;
        }
    }
}
