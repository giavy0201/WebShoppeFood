using BLL.IService;
using BLL.Models.DTOs.Comment;
using BLL.Models.Request.Comment;
using DAL.Entities;
using DAL.ModelsRequest;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(AddCommentRequest commentRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _commentService.AddComment(commentRequest);
                if (result == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Thêm bình luận thất bại"));
                }
                return Ok(ApiResponse<CommentDtos>.Success("Thêm bình luận thành công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Lỗi: {ex.Message}"));
            }
        }

        [HttpGet("{foodId}")]
        public async Task<IActionResult> GetCommentsByFoodId(int foodId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByFoodId(foodId);
                if (comments == null || !comments.Any())
                {
                    return NotFound(ApiResponse<string>.NotFound("Không tìm thấy bình luận cho món ăn này"));
                }
                return Ok(ApiResponse<List<CommentDtos>>.Success("Truy xuất bình luận thành công", comments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Lỗi: {ex.Message}"));
            }
        }

        [HttpGet("store/{storeId}")]
        public async Task<IActionResult> GetCommentsByStoreId(int storeId)
        {
            try
            {
                var comments = await _commentService.GetCommentsByStoreId(storeId);
                if (comments == null || !comments.Any())
                {
                    return NotFound(ApiResponse<string>.NotFound("Không tìm thấy bình luận cho cửa hàng này"));
                }
                return Ok(ApiResponse<List<CommentDtos>>.Success("Truy xuất bình luận thành công", comments));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Lỗi: {ex.Message}"));
            }
        }

        [HttpPut("{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentRequest updateCommentRequest)
        {
            try
            {
                if (updateCommentRequest == null || string.IsNullOrEmpty(updateCommentRequest.UpdatedContent))
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Nội dung bình luận không được để trống"));
                }
                double starRating = updateCommentRequest.StarRating == 0 ? 5.0 : updateCommentRequest.StarRating;

                var updatedComment = await _commentService.UpdateComment(commentId,updateCommentRequest.UpdatedContent,starRating);
                if (updatedComment == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Cập nhật bình luận thất bại"));
                }
                return Ok(ApiResponse<CommentDtos>.Success("Cập nhật bình luận thành công", updatedComment));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Lỗi: {ex.Message}"));
            }
        }

        [HttpDelete("{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId)
        {
            try
            {
                var success = await _commentService.DeleteComment(commentId);
                if (!success)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Xóa bình luận thất bại"));
                }
                return Ok(ApiResponse<string>.Success("Xóa bình luận thành công", null));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Lỗi: {ex.Message}"));
            }
        }
    }
}
