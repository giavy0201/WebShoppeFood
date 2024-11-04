using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request.Food
{
    public class UpdateFoodRequest
    {
        [Required(ErrorMessage = "Vui Lòng Chọn Mã Sản Phẩm")]
        public int? FoodId { get; set; }
        [Required(ErrorMessage = "Vui Lòng Nhập Tên")]
        [MaxLength(100, ErrorMessage = "Tên Sản Phẩm Vượt Quá 100 Kí Tự")]
        public string Name { get; set; }
        public string? Img { get; set; }
        [Required(ErrorMessage = "Vui Lòng Nhập Giá")]
        public double? Price { get; set; }
        [Range(0, 100, ErrorMessage = "Lỗi Giá Trị Giảm Giá (0-100)")]
        public int Discount { get; set; }
        [MaxLength(500, ErrorMessage = "Mô Tả Vượt Quá 500 Kí Tự")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Vui Lòng Chọn Menu")]
        public int? MenuID { get; set; }
        [MaxLength(50, ErrorMessage = "Tên Người Dùng Vượt Quá 50 Kí Tự")]
        public string? AdminName { get; set; }
        [Required(ErrorMessage = "Vui Lòng Chọn Trang Thái")]
        [Range(0, 1, ErrorMessage = "Vui Lòng Chọn 0 (InActive) hoặc 1 (Active)")]
        public int? Status { get; set; }
    }
}
