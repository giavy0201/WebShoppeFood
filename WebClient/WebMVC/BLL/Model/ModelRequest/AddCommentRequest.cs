using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model.ModelRequest
{
    public class AddCommentRequest
    {
        [Required(ErrorMessage = "Vui Lòng Nhập Nội Dung Bình Luận")]
        [MaxLength(1000, ErrorMessage = "Nội Dung Bình Luận Vượt Quá 1000 Kí Tự")]
        public string Content { get; set; } // Nội dung bình luận

        //[Required(ErrorMessage = "Vui Lòng Nhập ID Món Ăn")]
        public int FoodId { get; set; } // ID món ăn được bình luận

        [Required(ErrorMessage = "Vui Lòng Đánh Giá Sao")]
        [Range(0.0, 5.0, ErrorMessage = "Đánh Giá Sao Phải Nằm Trong Khoảng 0.0 Đến 5.0")]
        public double StarRating { get; set; } // Đánh giá sao của người dùng

        //[Required(ErrorMessage = "Vui Lòng Nhập ID Cửa Hàng")]
        public int StoreId { get; set; } // ID của cửa hàng liên quan đến món ăn

        //[Required(ErrorMessage = "Vui Lòng Nhập ID Khách Hàng")]
        public string CustomerId { get; set; } // ID của khách hàng bình luận
    }
}
