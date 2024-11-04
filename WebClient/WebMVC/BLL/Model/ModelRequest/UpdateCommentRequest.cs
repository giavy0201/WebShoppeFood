using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Model.ModelRequest
{
    public class UpdateCommentRequest
    {
        [Required(ErrorMessage = "Vui Lòng Cập Nhật Nội Dung Bình Luận")]
        [MaxLength(500, ErrorMessage = "Nội Dung Cập Nhật Vượt Quá 500 Kí Tự")]
        public string UpdatedContent { get; set; }

        [Required(ErrorMessage = "Vui Lòng Đánh Giá Sao")]
        [Range(0.0, 5.0, ErrorMessage = "Đánh Giá Sao Phải Nằm Trong Khoảng 0.0 Đến 5.0")]
        public double StarRating { get; set; } = 5.0;
    }
}
