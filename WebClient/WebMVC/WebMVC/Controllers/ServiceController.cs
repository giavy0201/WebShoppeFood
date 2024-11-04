using BLL.IService;
using BLL.Model.AddressDtos;
using BLL.Model.ModelRequest;
using BLL.Model.ModelStoreDtos;
using BLL.Model.ProductDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class ServiceController : Controller
    {
        private readonly IAddressService _addressService;
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly ICustomerService _customerService;
        public ServiceController(IAddressService addressService, IProductService productService, IStoreService storeService, ICustomerService customerService)
        {
            _addressService = addressService;
            _productService = productService;
            _storeService = storeService;
            _customerService = customerService;
        }


        /// <summary>
        /// List Collection
        /// </summary>
        /// <param name="reqCollection"></param>
        /// <returns></returns>
        public async Task<List<CollectionDtos>> ListCollection(ReqCollectionHome reqCollection)
        {
            var collections = await _productService.ListCollection(reqCollection);
            return collections.Data;
        }

        /// <summary>
        /// List Store Of Collection
        /// </summary>
        /// <param name="reqStoreCollection"></param>
        /// <returns></returns>
        public async Task<List<ListStoreOfCollecDtos>> StoreByCollection(ReqListStoreOfCollec reqStoreCollection)
        {
            var listStore = await _storeService.StoreByCollection(reqStoreCollection);
            return listStore.Data;
        }


        /// <summary>
        /// List Store Search
        /// </summary>
        /// <param name="reqSearch"></param>
        /// <returns></returns>
        public async Task<List<StoreDtos>> ListStoreSearch(ReqSearch reqSearch)
        {
            var listStore = await _storeService.ListStoreSearch(reqSearch);
            return listStore.Data;
        }

        /// <summary>
        /// Check Gmail
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<int> CheckGmail(string email)
        {
            var chekgmail = await _customerService.CheckGmail(email);
            return chekgmail;
        }

        /// <summary>
        /// List District By CityID
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public async Task<List<DistrictDtos>> ListDistrictByCity(int CityID)
        {
            var listDistricts = await _addressService.ListDistrictByCity(CityID);
            return listDistricts;
        }

        /// <summary>
        /// List Ward By DistrictID
        /// </summary>
        /// <param name="DistrictID"></param>
        /// <returns></returns>
        public async Task<List<WardDtos>> ListWardByDistrict(int DistrictID)
        {
            var listWards = await _addressService.ListWardByDistrict(DistrictID);
            return listWards;
        }

        /// <summary>
        /// List Store Preferential
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<List<StoreDtos>> ListStorePreferentialPage(ReqStorePreferential reqStorePre)
        {
            var listStore = await _storeService.ListStorePreferential(reqStorePre);
            return listStore.Data;
        }
    }
}
