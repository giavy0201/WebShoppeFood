using BLL.IService;
using BLL.Models.DTOs;
using BLL.Models.DTOs.ListMenu;
using BLL.Models.Request.ListMenu;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IStoreService _storeService;

        public MenuController(IMenuService menuService, IStoreService storeService)
        {
            _menuService = menuService;
            _storeService = storeService;
        }

        [HttpGet("Store/{StoreID}")]
        public async Task<IActionResult> ListMenuOfStore(int StoreID)
        {
            var listMenu = await _menuService.ListMenuOfStore(StoreID);
            if (!listMenu.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<ListMenuDtos>>.Success("Truy Xuất Thành Công", listMenu));
        }

        [HttpGet("Store/System/{StoreID}")]
        public async Task<IActionResult> ListMenuByStore(int StoreID)
        {
            var listMenu = await _menuService.GetListMenuByStore(StoreID);
            if (!listMenu.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<List<ListMenuSystem>>.Success("Truy Xuất Thành Công", listMenu));
        }

        [HttpGet("System/{MenuID}")]
        public async Task<IActionResult> DetailMenu(int MenuID)
        {
            var menu = await _menuService.DetailMenu(MenuID);
            if (menu == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<ListMenuSystem>.Success("Truy Xuất Thành Công", menu));
        }

        [HttpPost]
        public async Task<IActionResult> InsertMenu(AddMenuRequest addMenuRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var store = await _storeService.GetStoreById((int)addMenuRequest.StoreID);
                if (store == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Cửa Hàng Không Tồn Tại"));
                }
                var result = await _menuService.AddMenu(addMenuRequest);
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
        public async Task<IActionResult> UpdateMenu(UpdateMenuRequest updateMenuRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var selectMenu = await _menuService.DetailMenu((int)updateMenuRequest.MenuID);
                if (selectMenu == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Menu Không Tồn Tại"));
                }
                var result = await _menuService.UpdateMenu(updateMenuRequest);
                if (result == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Cập Nhập Thất Bại"));
                }
                else
                {
                    return Ok(ApiResponse<string>.SuccessCRUD("Cập Nhập Thành Công"));
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"{ex.Message}"));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenu(int MenuID)
        {
            var selectMenu = await _menuService.DetailMenu(MenuID);
            if (selectMenu == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Menu Không Tồn Tại"));
            }
            var menu = await _menuService.DeleteMenu(MenuID);
            if (menu == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Xóa Thất Bại"));
            }
            return Ok(ApiResponse<string>.Delete("Xóa Thành Công"));
        }

        [HttpGet("{MenuID}/Status")]
        public async Task<IActionResult> UpdateStatusMenu(int MenuID)
        {
            var selectMenu = await _menuService.DetailMenu(MenuID);
            if (selectMenu == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Menu Không Tồn Tại"));
            }
            var menu = await _menuService.UpdateMenuStatus(MenuID);
            if (menu == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Cập Nhập Thất Bại"));
            }
            return Ok(ApiResponse<ModelApiRequest>.Success("Cập Nhập Thành Công", menu));
        }
    }
}
