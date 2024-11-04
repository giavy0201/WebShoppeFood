using DAL.Entities;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;

namespace DAL.Non_Repository.AddressRepo
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IRepository<City> _cityRepo;
        private readonly IRepository<District> _districtRepo;
        private readonly IRepository<Ward> _wardRepo;
        private readonly DataContext _dataContext;

        public AddressRepository(IRepository<City> cityRepo, IRepository<District> districtRepo, IRepository<Ward> wardRepo, DataContext dataContext)
        {
            _cityRepo = cityRepo;
            _districtRepo = districtRepo;
            _wardRepo = wardRepo;
            _dataContext = dataContext;
        }
        public IEnumerable<City> ListCity()
        {
            return _cityRepo.GetAll();
        }
        public City GetCityByID(int CityID)
        {
            return _cityRepo.GetById(CityID);
        }
        public District GetDistrictByID(int DistrictID)
        {
            return _districtRepo.GetById(DistrictID);
        }
        public IEnumerable<District> ListDistrictByCity(int CityID)
        {
            return _districtRepo.GetAll().Where(x=>x.CityID == CityID).ToList();
        }
        public Ward GetWardByID(int WardID)
        {
            var ward = _dataContext.Wards.Include(x=>x.District).FirstOrDefault(x=>x.Id == WardID);
            return ward;
        }
        public IEnumerable<Ward> ListWardByCity(int CityID)
        {
            var listDistirctIdByCity = _districtRepo.GetAll().Where(x => x.CityID == CityID)
                                                       .Select(y=>y.Id).ToList();
            var listWardByCity =  _wardRepo.GetAll().Where(x => listDistirctIdByCity.Contains(x.DistrictID)).ToList();

            return listWardByCity;
        }
        public IEnumerable<Ward> ListWardByDistrict(int DistrictID)
        {
            var listWardIds = _wardRepo.GetAll().Where(x => x.DistrictID == DistrictID)
                                          .Select(y => y.Id).ToList();
            return _wardRepo.GetAll().Where(x => listWardIds.Contains(x.Id)).ToList();
        }
        public ViewModelLocation GetLocationByWard(int WardID)
        {
            var wards = _dataContext.Wards.Where(x => x.Id == WardID);
            var districts = _dataContext.Districts;
            var cities = _dataContext.Cities;
            var result = (from x in wards
                         join y in districts on x.DistrictID equals y.Id
                         join z in cities on y.CityID equals z.Id
                         select new ViewModelLocation
                         {
                             City = z.Name,
                             District = y.Name,
                             Ward = x.Name
                         }).First();
            return result;
        }
        public City GetCityByWard(int WardID)
        {
            var wards = _wardRepo.GetById(WardID);
            if (wards == null)
            {
                return null;
            }
            var districts = _districtRepo.GetById(wards.DistrictID);
            return _cityRepo.GetById(districts.CityID);
        }
        public async Task<ViewIDByWard> GetLocation(int WardID)
        {
            var wards = _dataContext.Wards.Where(x => x.Id == WardID);
            if(!wards.Any())
            {
                return null;
            }    
            var districts = _dataContext.Districts;
            var cities = _dataContext.Cities;
            var result = (from x in wards
                          join y in districts on x.DistrictID equals y.Id
                          join z in cities on y.CityID equals z.Id
                          select new ViewIDByWard
                          {
                              CityID = z.Id,
                              DistrictID = y.Id,
                              WardID = x.Id
                          }).First();
            return result;
        }
    }
}
