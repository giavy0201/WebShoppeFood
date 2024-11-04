using AutoMapper;
using BLL.IService;
using BLL.Models.DTOs;
using BLL.Models.DTOs.Comment;
using BLL.Models.Request.Comment;
using DAL.Entities;
using DAL.ModelsRequest;
using DAL.Non_Repository.CommentRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepo, IMapper mapper)
        {
            _commentRepo = commentRepo;
            _mapper = mapper;
        }

        public async Task<CommentDtos> AddComment(AddCommentRequest commentRequest)
        {
            try
            {
                var commentEntity = _mapper.Map<AddCommentReq>(commentRequest);
                var comment = await _commentRepo.AddComment(commentEntity);
                var commentDto = _mapper.Map<CommentDtos>(comment);
                return commentDto;
            }
            catch (Exception ex)
            {
                // Log error
                return null;
            }
        }

        public async Task<List<CommentDtos>> GetCommentsByFoodId(int foodId)
        {
            var comments = await _commentRepo.GetCommentsByFoodId(foodId);
            return _mapper.Map<List<CommentDtos>>(comments);
        }

        public async Task<List<CommentDtos>> GetCommentsByStoreId(int storeId)
        {
            var comments = await _commentRepo.GetCommentsByStoreId(storeId);
            return _mapper.Map<List<CommentDtos>>(comments);
        }

        public async Task<CommentDtos> UpdateComment(int commentId, string updatedContent, double StarRating)
        {


            var updatedComment = await _commentRepo.UpdateComment(commentId, updatedContent,StarRating);
            return _mapper.Map<CommentDtos>(updatedComment);

        }

        public async Task<bool> DeleteComment(int commentId)
        {
            return await _commentRepo.DeleteComment(commentId);
        }
    }
}
