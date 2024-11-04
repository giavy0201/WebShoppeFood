using AutoMapper;
using BLL.IService;
using BLL.Models;
using BLL.Models.DTOs.AddressDtos;
using DAL.Non_Repository.AddressRepo;

namespace BLL.Service
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepo;
        private readonly ICacheService _cacheService;
        private readonly IMapper _mapper;
        public AddressService(IAddressRepository addressRepo, IMapper mapper, ICacheService cacheService)
        {
            _addressRepo = addressRepo;
            _mapper = mapper;
            _cacheService = cacheService;
        }
        /// <summary>
        /// Select City by ID
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public async Task<CityDtos> GetCityById(int CityID)
        {
            var City = _addressRepo.GetCityByID(CityID);
            return _mapper.Map<CityDtos>(City);
        }
        /// <summary>
        /// Select District by ID
        /// </summary>
        /// <param name="DistrictID"></param>
        /// <returns></returns>
        public async Task<DistrictDtos> GetDistrictById(int DistrictID)
        {
            var district = _addressRepo.GetDistrictByID(DistrictID);
            return _mapper.Map<DistrictDtos>(district);

        }
        /// <summary>
        /// Select List District of City
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DistrictDtos>> ListDistrictOfCity(int CityID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListDistrictByCity, CityID.ToString());
            //var listDistrictCache = await _cacheService.GetCachedData<IEnumerable<DistrictDtos>>(stringKey);
            //if (listDistrictCache == null)
            //{
                var listdictrict = _addressRepo.ListDistrictByCity(CityID);
                var request = _mapper.Map<IEnumerable<DistrictDtos>>(listdictrict);
                //await _cacheService.SetCachedData(stringKey, request);
                return request;
            //}
            //return listDistrictCache;
        }
        /// <summary>
        /// Get all city
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CityDtos>> ListOfCity()
        {
            //var listCityCache = await _cacheService.GetCachedData<IEnumerable<CityDtos>>(GetKeyCache.ListCity);
            //if (listCityCache == null)
            //{
            //    var listCity = _addressRepo.ListCity();
            //    var city = _mapper.Map<IEnumerable<CityDtos>>(listCity);
            //    await _cacheService.SetCachedData(GetKeyCache.ListCity, city);
            //    return city;
            //}
            //return listCityCache;

            var listCity = _addressRepo.ListCity();
            var city = _mapper.Map<IEnumerable<CityDtos>>(listCity);
            return city;
        }
        /// <summary>
        /// Select Ward by ID
        /// </summary>
        /// <param name="WardID"></param>
        /// <returns></returns>
        public async Task<WardDtos> GetWardById(int WardID)
        {
            var ward = _addressRepo.GetWardByID(WardID);
            return _mapper.Map<WardDtos>(ward);
        }
        /// <summary>
        /// Select List Ward of City
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WardDtos>> ListWardOfCity(int CityID)
        {
            //var stringKey = GetKeyCache.CreateKey(GetKeyCache.ListWardByCity, CityID.ToString());
            //var listWardByCityCache = await _cacheService.GetCachedData<IEnumerable<WardDtos>>(stringKey);
            //if (listWardByCityCache == null)
            //{
            //    var listWard = _addressRepo.ListWardByCity(CityID);
            //    var request = _mapper.Map<IEnumerable<WardDtos>>(listWard);
            //    await _cacheService.SetCachedData(stringKey, request);
            //    return request;
            //}
            //return listWardByCityCache;
            var listWard = _addressRepo.ListWardByCity(CityID);
            var request = _mapper.Map<IEnumerable<WardDtos>>(listWard);
            return request;

        }
        /// <summary>
        /// Select List Ward of District
        /// </summary>
        /// <param name="DistrictID"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WardDtos>> ListWardOfDistrict(int DistrictID)
        {
            var listWard = _addressRepo.ListWardByDistrict(DistrictID);
            return _mapper.Map<IEnumerable<WardDtos>>(listWard);
        }
        public async Task<CityDtos> GetCityByWard(int WardID)
        {
            var city = _addressRepo.GetCityByWard(WardID);
            return _mapper.Map<CityDtos>(city);
        }
        public async Task<ViewIDByWard> GetLoctionIDByWard(int WardID)
        {
            var listID = await _addressRepo.GetLocation(WardID);
            return listID;
        }
    }
}
