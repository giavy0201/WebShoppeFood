using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.DTOs.Comment
{
    public class CommentDtos
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int FoodId { get; set; }
        public double StarRating { get; set; }
        public string CustomerId { get; set; }
        public string FirstName { get; set; } // Add this line
        public string LastName { get; set; }  // Add this line
        public int Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int StoreId { get; set; }
    }
}
