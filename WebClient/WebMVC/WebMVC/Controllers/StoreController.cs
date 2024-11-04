using BLL.IService;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Formats.Asn1.AsnWriter;

namespace WebMVC.Controllers
{
    public class StoreController : Controller
    {
        private readonly IStoreService _storeService;
        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        /// <summary>
        /// UI Detail Store
        /// </summary>
        /// <param name="StoreID"></param>
        /// <returns></returns>
        [HttpGet("Store/{StoreID}")]
        public async Task<ActionResult> DetailStore(int StoreID)
        {
            try
            {
                ViewBag.StoreID = StoreID;
                var store = await _storeService.DetailStore(StoreID);
                
                if (store == null)
                {
                    return PartialView("~/Views/Shared/PageNotFound.cshtml");
                }
                var foodItems = await _storeService.ListFoodOfStore(StoreID);
                ViewBag.FoodID = foodItems?.FirstOrDefault()?.Id;
                var Price = await _storeService.ListFoodOfStore(StoreID);
                if (Price == null || !Price.Any())
                {
                    ViewBag.PriceMax = 0;
                    ViewBag.PriceMin = 0;
                }
                else
                {
                    ViewBag.PriceMax = Price?.Select(x => x.Price).Max();
                    ViewBag.PriceMin = Price?.Select(x => x.Price).Min();
                }
                var dayTime = DateTime.Now.TimeOfDay;
                if (store.TimeClose < store.TimeOpen)
                {
                    if (dayTime > store.TimeOpen || dayTime < store.TimeClose)
                    {
                        ViewBag.StatusID = 1;
                        ViewBag.Status = "Mở Cửa";
                        ViewBag.Color = "green";
                    }
                    else
                    {
                        ViewBag.StatusID = 0;
                        ViewBag.Status = "Đóng Cửa";
                        ViewBag.Color = "red";
                    }
                }
                else
                {
                    if (store.TimeOpen < dayTime && dayTime < store.TimeClose)
                    {
                        ViewBag.StatusID = 1;
                        ViewBag.Status = "Mở Cửa";
                        ViewBag.Color = "green";
                    }
                    else
                    {
                        ViewBag.StatusID = 0;
                        ViewBag.Status = "Đóng Cửa";
                        ViewBag.Color = "red";
                    }
                }

                return View(store);
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }

        }
        
    }
}
