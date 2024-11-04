using BLL.Models.DTOs.AddressDtos;
using DAL.Non_Repository.AddressRepo;

namespace BLL.IService
{
    public interface IAddressService
    {
        Task<IEnumerable<CityDtos>> ListOfCity();
        Task<CityDtos> GetCityById(int CityID);
        Task<IEnumerable<DistrictDtos>> ListDistrictOfCity(int CityID);
        Task<DistrictDtos> GetDistrictById(int DistrictID);
        Task<IEnumerable<WardDtos>> ListWardOfCity(int CityID);
        Task<WardDtos> GetWardById(int DistrictID);
        Task<IEnumerable<WardDtos>> ListWardOfDistrict(int DistrictID);
        Task<CityDtos> GetCityByWard(int WardID);
        Task<ViewIDByWard> GetLoctionIDByWard(int WardID);
    }
}
