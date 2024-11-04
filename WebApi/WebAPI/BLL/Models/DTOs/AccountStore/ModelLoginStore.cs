using System.ComponentModel.DataAnnotations;

namespace BLL.Models.DTOs.AccountStore
{
    public class ModelLoginStore
    {
        [Required(ErrorMessage = "Vui Lòng Nhập Tên Đăng Nhập")]
        public string LoginName { get; set; }
        [Required(ErrorMessage = "Vui Lòng Nhập Mật Khẩu")]
        public string Password { get; set; }
    }
}
