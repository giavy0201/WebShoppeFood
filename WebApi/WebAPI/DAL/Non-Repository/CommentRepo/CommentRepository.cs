using DAL.Entities;
using DAL.ModelsRequest;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Non_Repository.CommentRepo
{
    public class CommentRepository : ICommentRepository
    {
        private readonly IRepository<Comment> _commentRepo;
        private readonly DataContext _dataContext;

        public CommentRepository(IRepository<Comment> commentRepo, DataContext dataContext)
        {
            _commentRepo = commentRepo;
            _dataContext = dataContext;
        }

        public async Task<Comment> AddComment(AddCommentReq commentRequest)
        {
            //var newComment = new Comment
            //{
            //    Content = commentRequest.Content,
            //    FoodId = commentRequest.FoodId,   // Change FoodID to FoodId
            //    //UserId = commentRequest.UserId, // Change UserID to UserId
            //    StarRating = commentRequest.StarRating,
            //    CreatedAt = DateTime.UtcNow,
            //    UpdatedAt = DateTime.UtcNow,
            //    Status = ValueGeneric.Active,
            //    StoreId = commentRequest.StoreId

            //};

            //_commentRepo.Insert(newComment);
            //_dataContext.SaveChangesAsync();
            //return newComment;
            var customer = await _dataContext.Users
       .Where(u => u.Id == commentRequest.CustomerId)
       .Select(u => new { u.Id,u.FirstName, u.LastName })
       .FirstOrDefaultAsync();

            // Check if the customer exists
            if (customer == null)
            {
                throw new Exception("Khách hàng không tồn tại");
            }

            var newComment = new Comment
            {
                Content = commentRequest.Content,
                FoodId = commentRequest.FoodId,
                CustomerId = customer.Id,
                StarRating = commentRequest.StarRating,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                Status = ValueGeneric.Active,
                StoreId = commentRequest.StoreId,
                FirstName = customer.FirstName, // Bạn cần thêm thuộc tính này vào lớp Comment
                LastName = customer.LastName, // Bạn cần thêm thuộc tính này vào lớp Comment

            };

            // Insert the new comment into the repository
             _commentRepo.Insert(newComment); // Make sure this is async
            await _dataContext.SaveChangesAsync(); // Save changes asynchronously

            return newComment;
        }

        public async Task<List<Comment>> GetCommentsByFoodId(int foodId)
        {
            var comments = await _dataContext.Comments
                .Where(c => c.FoodId == foodId && c.Status == ValueGeneric.Active)
                .Include(c => c.Customer) // Gọi đến Customer để lấy FirstName và LastName
                .Select(c => new Comment
                {
                    Id = c.Id,
                    Content = c.Content,
                    FoodId = c.FoodId,
                    StarRating = c.StarRating,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Status = c.Status,
                    StoreId = c.StoreId,
                    CustomerId= c.Customer.Id,
                    FirstName = c.Customer.FirstName,
                    LastName = c.Customer.LastName
                })
                .ToListAsync();

            return comments;
            //return _dataContext.Comments
            //    .Where(c => c.FoodId == foodId && c.Status == ValueGeneric.Active)
            //    .ToList();
        }

        public async Task<Comment> UpdateComment(int commentId, string updatedContent, double StarRating)
        {
            //var comment =  _commentRepo.GetById(commentId);
            //if (comment == null || comment.Status != ValueGeneric.Active)
            //{
            //    return null;
            //}

            //comment.Content = updatedContent;
            //comment.StarRating = StarRating;
            //comment.UpdatedAt = DateTime.UtcNow;

            //_commentRepo.Update(comment);
            //await _dataContext.SaveChangesAsync();
            //return comment;
            var comment = await _dataContext.Comments
         .Include(c => c.Customer) // Include để lấy Customer với FirstName và LastName
         .FirstOrDefaultAsync(c => c.Id == commentId);

            // Kiểm tra xem comment có tồn tại và đang ở trạng thái Active
            if (comment == null || comment.Status != ValueGeneric.Active)
            {
                return null;
            }

            // Cập nhật nội dung comment
            comment.Content = updatedContent;
            comment.StarRating = StarRating;
            comment.UpdatedAt = DateTime.UtcNow;

            // Cập nhật comment trong repository
            _commentRepo.Update(comment);
            await _dataContext.SaveChangesAsync();

            // Lấy FirstName và LastName từ Customer nếu có
            comment.FirstName = comment.Customer?.FirstName;
            comment.LastName = comment.Customer?.LastName;

            return comment;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            //var comment =  _commentRepo.GetById(commentId);
            //if (comment == null || comment.Status != ValueGeneric.Active)
            //{
            //    return false;
            //}

            //comment.Status = ValueGeneric.Inactive;
            //_commentRepo.Update(comment);
            //await _dataContext.SaveChangesAsync();
            //return true;
            var comment =  _commentRepo.GetById(commentId);
            if (comment == null || comment.Status != ValueGeneric.Active)
            {
                return false;
            }

            comment.Status = ValueGeneric.Inactive;
             _commentRepo.Update(comment);
            await _dataContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Comment>> GetCommentsByStoreId(int storeId)
        {
            //return  _dataContext.Comments
            //    .Where(c => c.StoreId == storeId && c.Status == ValueGeneric.Active)
            //    .ToList();
            var comments = await _dataContext.Comments
                .Where(c => c.StoreId == storeId && c.Status == ValueGeneric.Active)
                .Include(c => c.Customer) // Gọi đến Customer để lấy FirstName và LastName
                .Select(c => new Comment
                {
                    Id = c.Id,
                    Content = c.Content,
                    //FoodId = c.FoodId,
                    StarRating = c.StarRating,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Status = c.Status,
                    StoreId = c.StoreId,
                    CustomerId = c.Customer.Id,
                    FirstName = c.Customer.FirstName,
                    LastName = c.Customer.LastName
                })
                .ToListAsync();

            return comments;
        }
    }
}
