using BLL.IService;
using BLL.Models.DTOs.CollectionDtos;
using BLL.Models.Request;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CollectionsController : Controller
    {
        private readonly ICollectionService _collectionService;

        public CollectionsController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        [HttpPost]
        public async Task<IActionResult> ListCollecByCityCate(SearchCollectionHomeRequest searchCollectionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _collectionService.ListCollectionByCityCate(searchCollectionRequest);
                if (!result.Any())
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }
                return Ok(ApiResponse<IEnumerable<CollecDtos>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }

        [HttpGet("{CollectionID}")]
        public async Task<IActionResult> GetCollectionByID(int CollectionID)
        {
            var collection = await _collectionService.GetCollectionById(CollectionID);
            if (collection == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<CollecDtos>.Success("Truy Xuất Thành Công", collection));
        }

        [HttpPost("Stores")]
        public async Task<IActionResult> ListStoreByCollection(StoreCollectionRequest StoreCollectionRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _collectionService.ListStoreByCollection(StoreCollectionRequest);
                if (!result.Any())
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }
                return Ok(ApiResponse<IEnumerable<ListStoreOfCollectionDtos>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }
    }
}
