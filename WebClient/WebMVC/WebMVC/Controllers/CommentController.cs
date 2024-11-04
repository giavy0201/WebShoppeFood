using BLL.IService;
using BLL.Model.Cart;
using BLL.Model.Comment;
using BLL.Model.ModelRequest;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
//using Microsoft.AspNetCore.Identity;
namespace WebMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IHttpContextAccessor _context;

        public CommentController(ICommentService commentService, IHttpContextAccessor context)
        {
            _commentService = commentService;
            _context = context;
        }

        [HttpGet("Comments")]
        public async Task<IActionResult> CommentsPage(int storeId)
        {
            var customerId = _context.HttpContext.Session.GetString("customerID");
            if (string.IsNullOrEmpty(customerId))
            {
                // Optionally redirect to login if customer is not authenticated
                return RedirectToAction("Login", "Customer");
            }
            var comments = await _commentService.GetListCommentsByStoreId(storeId);
            var viewModel = new CommentsPageViewModel
            {
                Comments = comments ?? new List<CommentDtos>(),
                AddCommentRequest = new AddCommentRequest
                {
                    StoreId = storeId,
                    CustomerId = customerId,
                    StarRating = 5.0
                }
            };

            return View(viewModel);
        }

        [HttpPost("Comments/Add")]
        public async Task<IActionResult> AddComment([FromBody] AddCommentRequest addCommentRequest)
        {
            //try
            //{
            //    if (addCommentRequest == null)
            //    {
            //        return BadRequest("Yêu cầu không hợp lệ.");
            //    }
            //    if (int.TryParse(_context.HttpContext.Session.GetString("customerID"), out int customerId))
            //    {
            //        addCommentRequest.CustomerId = customerId.ToString();
            //    }
            //    else
            //    {
            //        return Unauthorized("Vui lòng đăng nhập để thêm bình luận.");
            //    }

            //    if (!ModelState.IsValid)
            //    {
            //        var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
            //        return BadRequest(new { Message = "Dữ liệu không hợp lệ.", Errors = errors });
            //    }

            //    var result = await _commentService.AddComment(addCommentRequest);
            //    if (result.IsSuccess)
            //    {
            //        var comments = await _commentService.GetListCommentsByStoreId(addCommentRequest.StoreId);
            //        return PartialView("_listCommentsByStore1", comments);
            //    }
            //    else
            //    {
            //        return BadRequest("Không thể thêm bình luận.");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Lỗi khi thêm bình luận: {ex.Message}");
            //}
            try
    {
                // Validate the incoming request
                if (addCommentRequest == null)
                {
                    return BadRequest("Yêu cầu không hợp lệ."); // Invalid request
                }

                // Retrieve the customer ID from the session
                var customerId = _context.HttpContext.Session.GetString("customerID");
                if (string.IsNullOrEmpty(customerId))
                {
                    return Unauthorized("Vui lòng đăng nhập để thêm bình luận."); // Unauthorized
                }
                addCommentRequest.CustomerId = customerId;

                // Check if the model state is valid
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    Console.WriteLine($"Model errors: {string.Join(", ", errors)}");
                    return BadRequest(new { Message = "Dữ liệu không hợp lệ.", Errors = errors });
                }
                Console.WriteLine($"Adding Comment: StoreId={addCommentRequest.StoreId}, CustomerId={addCommentRequest.CustomerId}, Content={addCommentRequest.Content}, StarRating={addCommentRequest.StarRating}");
                // Attempt to add the comment
                var result = await _commentService.AddComment(addCommentRequest);
                if (result.IsSuccess)
                {
                    return PartialView("~/Views/Store/DetailStore.cshtml");
                }

                // If the comment could not be added, return an error
                return BadRequest("Không thể thêm bình luận."); // Could not add comment
            }
    catch (Exception ex)
    {
                // Log the exception and return a server error
                Console.WriteLine($"Exception: {ex}");
                return StatusCode(500, $"Lỗi khi thêm bình luận: {ex.Message}"); // Server error
            }
        }

        [HttpPost("Comments/Update/{commentId}")]
        public async Task<IActionResult> UpdateComment(int commentId, [FromBody] UpdateCommentRequest updateCommentRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = await _commentService.UpdateComment(commentId, updateCommentRequest);
                if (result.IsSuccess)
                {
                    var comments = await _commentService.GetListCommentsByStoreId(commentId);
                    return PartialView("_listCommentsByStore1", comments);
                }
                else
                {
                    return BadRequest("Không thể cập nhật bình luận.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi cập nhật bình luận: {ex.Message}");
            }
        }

        [HttpPost("Comments/Delete/{commentId}")]
        public async Task<IActionResult> DeleteComment(int commentId, int storeId)
        {
            try
            {
                var success = await _commentService.DeleteComment(commentId);
                if (success)
                {
                    //var comments = await _commentService.GetListCommentsByStoreId(storeId);
                    return PartialView("~/Views/Store/DetailStore.cshtml");
                }
                else
                {
                    return BadRequest("Không thể xóa bình luận.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi xóa bình luận: {ex.Message}");
            }
        }

        public async Task<ActionResult> ListCommentsByStore(int storeId)
        {
            try
            {
                var comments = await _commentService.GetListCommentsByStoreId(storeId);
                if (comments != null && comments.Any())
                {
                    return PartialView("_listCommentsByStore1", comments);
                }
                else
                {
                    return PartialView("~/Views/Shared/_dataEmpty.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }

        public async Task<ActionResult> ListCommentsByFood(int foodId)
        {
            try
            {
                var comments = await _commentService.GetListCommentsByFoodId(foodId);
                if (comments != null && comments.Any())
                {
                    return PartialView("_listCommentsByFood", comments);
                }
                else
                {
                    return PartialView("~/Views/Shared/_dataEmpty.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }

    }
}
