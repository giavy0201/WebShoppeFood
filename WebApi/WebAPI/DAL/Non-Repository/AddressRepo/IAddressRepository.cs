using DAL.Entities;

namespace DAL.Non_Repository.AddressRepo
{
    public interface IAddressRepository
    {
        IEnumerable<City> ListCity();
        City GetCityByID(int CityID);
        District GetDistrictByID(int DistrictID);
        IEnumerable<District> ListDistrictByCity(int CityID);
        Ward GetWardByID(int WardID);
        IEnumerable<Ward> ListWardByCity(int CityID);
        IEnumerable<Ward> ListWardByDistrict(int DistrictID);
        ViewModelLocation GetLocationByWard(int WardID);
        City GetCityByWard(int WardID);
        Task<ViewIDByWard> GetLocation(int WardID);
    }
}
