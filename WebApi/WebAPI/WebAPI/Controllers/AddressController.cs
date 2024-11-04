using BLL.IService;
using BLL.Models.DTOs.AddressDtos;
using DAL.Non_Repository.AddressRepo;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("Cities")]
        public async Task<IActionResult> ListCity()
        {
            var listCity = await _addressService.ListOfCity();
            if (listCity == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<CityDtos>>.Success("Truy Xuất Thành Công", listCity));
        }

        [HttpGet("City/{CityID}")]
        public async Task<IActionResult> GetCityByID(int CityID)
        {
            var City = await _addressService.GetCityById(CityID);
            if (City == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<CityDtos>.Success("Truy Xuất Thành Công", City));
        }

        [HttpGet("City/{CityID}/District")]
        public async Task<IActionResult> ListDistrictCity(int CityID)
        {
            var districts = await _addressService.ListDistrictOfCity(CityID);
            if (!districts.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<DistrictDtos>>.Success("Truy Xuất Thành Công", districts));
        }

        [HttpGet("City/{CityID}/Wards")]
        public async Task<IActionResult> ListWardOfCity(int CityID)
        {
            var Wards = await _addressService.ListWardOfCity(CityID);
            if (!Wards.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<WardDtos>>.Success("Truy Xuất Thành Công", Wards));
        }

        [HttpGet("District/{DistrictID}")]
        public async Task<IActionResult> GetDistrictByID(int DistrictID)
        {
            var district = await _addressService.GetDistrictById(DistrictID);
            if (district == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<DistrictDtos>.Success("Truy Xuất Thành Công", district));
        }

        [HttpGet("District/{DistrictID}/Wards")]
        public async Task<IActionResult> ListWardOfDistrict(int DistrictID)
        {
            var Wards = await _addressService.ListWardOfDistrict(DistrictID);
            if (!Wards.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<WardDtos>>.Success("Truy Xuất Thành Công", Wards));
        }

        [HttpGet("Ward/{WardID}")]
        public async Task<IActionResult> GetWardByID(int WardID)
        {
            var ward = await _addressService.GetWardById(WardID);
            if (ward == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<WardDtos>.Success("Truy Xuất Thành Công", ward));
        }

        [HttpGet("City/Ward/{WardID}")]
        public async Task<IActionResult> GetCityByWard(int WardID)
        {
            var city = await _addressService.GetCityByWard(WardID);
            if (city == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<CityDtos>.Success("Truy Xuất Thành Công", city));
        }
        [HttpGet("Location/Ward/{WardID}")]
        public async Task<IActionResult> ListLocationIDByWard(int WardID)
        {
            var locations = await _addressService.GetLoctionIDByWard(WardID);
            if (locations == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<ViewIDByWard>.Success("Truy Xuất Thành Công", locations));
        }
    }
}
