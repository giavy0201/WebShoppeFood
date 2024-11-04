using BLL.IService;
using BLL.Models.DTOs;
using BLL.Models.DTOs.Food;
using BLL.Models.Request;
using BLL.Models.Request.Food;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class FoodController : Controller
    {
        private readonly IMenuService _menuService;

        public FoodController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet("Store/{StoreID}")]
        public async Task<IActionResult> ListMenuFoodOfStore(int StoreID)
        {
            var listfood = await _menuService.ListMenuFoodOfStore(StoreID);
            if (!listfood.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<ListMenuFoodOfStoreDtos>>.Success("Truy Xuất Thành Công", listfood));
        }

        [HttpGet("Menu/{MenuID}")]
        public async Task<IActionResult> ListFoodOfMenu(int MenuID)
        {
            var listfood = await _menuService.ListFoodOfMenu(MenuID);
            if (!listfood.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<FoodDtos>>.Success("Truy Xuất Thành Công", listfood));
        }

        [HttpGet("{FoodID}")]
        public async Task<IActionResult> DetailFood(int FoodID)
        {
            var food = await _menuService.DeatailFood(FoodID);
            if (food == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<FoodDtos>.Success("Truy Xuất Thành Công", food));
        }

        [HttpPost("Search")]
        public async Task<IActionResult> SearchProduct(SelectProductRequest searchSelectRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }

                var result = await _menuService.SearchProduct(searchSelectRequest);

                if (result == null || result.Count == 0)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }

                return Ok(ApiResponse<List<FoodSystem>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }
        [HttpGet("Store/System/{StoreID}")]
        public async Task<IActionResult> ListFoodByStore(int StoreID)
        {
            var listfood = await _menuService.GetListFoodByStore(StoreID);
            if (!listfood.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<List<FoodSystem>>.Success("Truy Xuất Thành Công", listfood));
        }

        [HttpGet("System/Menu/{MenuID}")]
        public async Task<IActionResult> ListFoodByMenu(int MenuID)
        {
            var listfood = await _menuService.GetListFoodByMenu(MenuID);
            if (!listfood.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<List<FoodSystem>>.Success("Truy Xuất Thành Công", listfood));
        }

        [HttpGet("System/{FoodID}")]
        public async Task<IActionResult> DetailFoodAdmin(int FoodID)
        {
            var food = await _menuService.DetailFoodAdmin(FoodID);
            if (food == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<FoodSystem>.Success("Truy Xuất Thành Công", food));
        }

        [HttpPost]
        public async Task<IActionResult> InsertFood(AddFoodRequest addFoodRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var menu = await _menuService.DetailMenu((int)addFoodRequest.MenuID);
                if (menu == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Menu Không Tồn Tại"));
                }
                var result = await _menuService.AddFood(addFoodRequest);
                if (result == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Thêm Thất Bại"));
                }
                else
                {
                    return Ok(ApiResponse<string>.SuccessCRUD("Thêm Thành Công"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"{ex.Message}"));
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFood(UpdateFoodRequest modelFood)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var FoodID = await _menuService.DeatailFood((int)modelFood.FoodId);
                if (FoodID == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Sản Phẩm Không Tồn Tại"));
                }
                var menu = await _menuService.DetailMenu((int)modelFood.MenuID);
                if (menu == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Menu Không Tồn Tại"));
                }
                var result = await _menuService.UpdateFood(modelFood);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteFood(int FoodID)
        {
            var selectFood = await _menuService.DetailFoodAdmin(FoodID);
            if (selectFood == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Sản Phẩm Không Tồn Tại"));
            }
            var food = await _menuService.DeleteFood(FoodID);
            if (food == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Xóa Thất Bại"));
            }
            return Ok(ApiResponse<string>.Delete("Xóa Thành Công"));
        }

        [HttpGet("{FoodID}/Status")]
        public async Task<IActionResult> UpdateStatusFood(int FoodID)
        {
            var selectFood = await _menuService.DetailFoodAdmin(FoodID);
            if (selectFood == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Sản Phẩm Không Tồn Tại"));
            }
            var food = await _menuService.UpdateFoodStatus(FoodID);
            if (food == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Cập Nhập Thất Bại"));
            }
            return Ok(ApiResponse<ModelApiRequest>.Success("Cập Nhập Thành Công", food));
        }
    }
}
