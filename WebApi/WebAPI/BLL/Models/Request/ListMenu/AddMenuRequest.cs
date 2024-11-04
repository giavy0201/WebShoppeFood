using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request.ListMenu
{
    public class AddMenuRequest
    {
        [Required(ErrorMessage = "Vui Lòng Nhập Tên")]
        [MaxLength(100, ErrorMessage = "Tên Menu Vượt Quá 100 Kí Tự")]
        public string MenuName { get; set; }
        [Required(ErrorMessage = "Vui Lòng Chọn Mã Cửa Hàng Muốn Thêm Sản Phẩm")]
        public int? StoreID { get; set; }
        [MaxLength(50, ErrorMessage = "Tên Người Dùng Vượt Quá 50 Kí Tự")]
        public string? AdminName { get; set; }
        [Range(0, 1, ErrorMessage = "Vui Lòng Chọn 0 (InActive) hoặc 1 (Active)")]
        public int? Status { get; set; }
    }
}
