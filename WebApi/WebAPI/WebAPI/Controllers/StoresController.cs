using BLL.IService;
using BLL.Models.DTOs;
using BLL.Models.DTOs.AccountStore;
using BLL.Models.Request;
using BLL.Models.Request.Store;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;
using static BLL.Models.Validate.ValidateGeneric;

namespace WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class StoresController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly ICartService _cartService;
        private readonly IAddressService _addressService;
        private readonly IProductService _productService;

        public StoresController(IStoreService storeService, ICartService cartService, IAddressService addressService, IProductService productService)
        {
            _storeService = storeService;
            _cartService = cartService;
            _addressService = addressService;
            _productService = productService;
        }

        [HttpGet("{StoreID}")]
        public async Task<IActionResult> GetStoreByID(int StoreID)
        {
            var store = await _storeService.GetStoreById(StoreID);
            if (store == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<StoreDtos>.Success("Truy Xuất Thành Công", store));
        }

        [HttpPost("Preferential")]
        public async Task<IActionResult> ListStorePreByCity(StoreOfCityCateRequest searchStoreRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _storeService.ListStorePreByCityAndCate(searchStoreRequest);
                if (!result.Any())
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }
                return Ok(ApiResponse<IEnumerable<StoreDtos>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }

        [HttpPost("Search")]
        public async Task<IActionResult> ListStoreSearch(SearchStoreRequest searchStoreRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _storeService.ListStoreSearch(searchStoreRequest);
                if (!result.Any())
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }
                return Ok(ApiResponse<IEnumerable<StoreDtos>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }

        [HttpPost("District")]
        public async Task<IActionResult> ListStoreMenuBot(StoreMenuBotRequest searchStoreRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _storeService.ListStoreByDistrict(searchStoreRequest);
                if (!result.Any())
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }
                return Ok(ApiResponse<IEnumerable<StoreDtos>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }
        //system admin store

        [HttpPost("Login")]
        public async Task<IActionResult> LoginSystemStore(ModelLoginStore loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _storeService.LoginSystemStore(loginRequest);
                if (result.StatusCode == 0)
                {
                    return BadRequest(ApiResponse<string>.BadRequest(result.StatusMessage));
                }
                return Ok(ApiResponse<ViewLogin>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }

        [HttpGet("System/{StoreID}")]
        public async Task<IActionResult> InfoStoreSystem(int StoreID)
        {
            var store = await _storeService.SystemInfoStore(StoreID);
            if (store == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<StoreSystem>.Success("Truy Xuất Thành Công", store));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateInfoStore(UpdateStoreRequest updateStoreRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var StoreID = await _storeService.GetStoreById(updateStoreRequest.StoreID);
                if (StoreID == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Cửa Hàng Không Tồn Tại"));
                }
                if (!IsTimeValid(updateStoreRequest.TimeOpen) || !IsTimeValid(updateStoreRequest.TimeClose))
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Nhập Giờ Không Hợp Lệ"));
                }
                var WardID = await _addressService.GetWardById((int)updateStoreRequest.WardID);
                if (WardID == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Phường Không Tồn Tại"));
                }
                var ContentID = _productService.GetContentById((int)updateStoreRequest.ContentID);
                if (ContentID == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Loại Cửa Hàng Không Tồn Tại"));
                }
                var result = await _storeService.UpdateInfoStore(updateStoreRequest);
                if (result == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Cập Nhập Thất Bại"));
                }
                return Ok(ApiResponse<string>.SuccessCRUD("Cập Nhập Thành Công"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"{ex.Message}"));
            }
        }

        [HttpGet("{StoreID}/Status")]
        public async Task<IActionResult> UpdateStoreStatus(int StoreID)
        {
            var status = await _storeService.UpdateStoreStatus(StoreID);
            if (status == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<StoreSystem>.Success("Cập Nhập Thành Công", status));
        }

        [HttpGet("{StoreID}/Today/TotalMoney")]
        public async Task<ActionResult<double>> TotalMoneyToday(int StoreID)
        {
            return await _cartService.TotalMoneyToday(StoreID);
        }
    }
}
