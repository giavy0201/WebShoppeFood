using BLL.IService;
using BLL.Models.DTOs.ProductDtos;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("Categories")]
        public async Task<IActionResult> ListCategory()
        {
            var listCate = await _productService.ListCategory();
            if (listCate == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<CateDtos>>.Success("Truy Xuất Thành Công", listCate));
        }

        [HttpGet("Category/{CategoryID}")]
        public async Task<IActionResult> GetCateByID(int CategoryID)
        {
            var Category = await _productService.GetCategorById(CategoryID);
            if (Category == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<CateDtos>.Success("Truy Xuất Thành Công", Category));
        }

        [HttpGet("Category/{CategoryID}/Contents")]
        public async Task<IActionResult> ListContentByCate(int CategoryID)
        {
            var listContent = await _productService.ListContentOfCategory(CategoryID);
            if (!listContent.Any())
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<ContentProductDtos>>.Success("Truy Xuất Thành Công", listContent));
        }

        [HttpGet("Contents")]
        public async Task<IActionResult> ListContent()
        {
            var listContent = await _productService.GetListContent();
            if (listContent == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<IEnumerable<ContentProductDtos>>.Success("Truy Xuất Thành Công", listContent));
        }

        [HttpGet("Content/{ContentID}")]
        public async Task<IActionResult> GetContentByID(int ContentID)
        {
            var content = await _productService.GetContentById(ContentID);
            if (content == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<ContentProductDtos>.Success("Truy Xuất Thành Công", content));
        }
    }
}
