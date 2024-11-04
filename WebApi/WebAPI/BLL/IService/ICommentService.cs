using DAL.Entities;
using DAL.ModelsRequest;
using BLL.Models.DTOs.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Models.Request.Comment;

namespace BLL.IService
{
    public interface ICommentService
    {
        Task<CommentDtos> AddComment(AddCommentRequest commentRequest);
        Task<List<CommentDtos>> GetCommentsByFoodId(int foodId);
        Task<List<CommentDtos>> GetCommentsByStoreId(int storeId);
        Task<CommentDtos> UpdateComment(int commentId, string updatedContent, double StarRating);
        Task<bool> DeleteComment(int commentId);
    }
}
