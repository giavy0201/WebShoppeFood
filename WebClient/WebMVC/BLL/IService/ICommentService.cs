using BLL.Model.Comment;
using BLL.Model;
using BLL.Model.ModelRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.IService
{
    public interface ICommentService
    {
        Task<List<CommentDtos>> GetListCommentsByFoodId(int foodId);

        Task<List<CommentDtos>> GetListCommentsByStoreId(int storeId);
        // Add a new comment
        Task<ApiResponse<CommentDtos>> AddComment(AddCommentRequest addCommentRequest);

        // Update an existing comment by commentId
        Task<ApiResponse<CommentDtos>> UpdateComment(int commentId, UpdateCommentRequest updateCommentRequest);

        // Get details of a specific comment by commentId
        Task<bool> DeleteComment(int commentId);
    }
}
