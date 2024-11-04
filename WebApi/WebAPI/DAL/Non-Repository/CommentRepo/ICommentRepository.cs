using DAL.Entities;
using DAL.ModelsRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Non_Repository.CommentRepo
{
    public interface ICommentRepository
    {
        Task<Comment> AddComment(AddCommentReq commentRequest);
        Task<List<Comment>> GetCommentsByFoodId(int foodId);
        Task<Comment> UpdateComment(int commentId, string updatedContent, double StarRating); 
        Task<bool> DeleteComment(int commentId);
        Task<List<Comment>> GetCommentsByStoreId(int storeId);
    }
}
