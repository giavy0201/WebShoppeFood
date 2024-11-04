using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request.Store
{
    public class UpdateStoreRequest
    {
        [Required(ErrorMessage = "Bắt Buộc Có Mã Cửa Hàng")]
        public int StoreID { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Tên")]
        [MaxLength(150, ErrorMessage = "Tên Cửa Hàng Vượt Quá 150 Ký Tự")]
        public string Name { get; set; }
        public string? Img { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Thời Gian Mở Cửa")]
        public string TimeOpen { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Thời Gian Đóng Cửa")]
        public string TimeClose { get; set; }
        [MaxLength(50, ErrorMessage = "Tên Ưu Đãi Vượt Quá 50 Ký Tự")]
        public string? Preferential { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Địa Chỉ")]
        [MaxLength(150, ErrorMessage = "Địa Chỉ Vượt Quá 150 Ký Tự")]
        public string Location { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Mã Phường")]
        public int? WardID { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Có Loại Cửa Hàng")]
        public int? ContentID { get; set; }
        [Required(ErrorMessage = "Bắt Buộc Phải Thông Tin Người Dùng")]
        [MaxLength(50, ErrorMessage = "Tên Người Dùng Vượt Quá 50 Kí Tự")]
        public string AdminName { get; set; }
        [Range(0, 2)]
        public int? Status { get; set; }
    }
}
