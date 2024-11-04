using BLL.Model.AddressDtos;

namespace BLL.IService
{
    public interface IAddressService
    {
        Task<List<CityDtos>> GetListCity();
        Task<List<WardDtos>> ListWardByDistrict(int DistrictID);
        Task<List<DistrictDtos>> ListDistrictByCity(int CityID);
        Task<CityDtos> GetCityByWard(int CityID);
    }
}
