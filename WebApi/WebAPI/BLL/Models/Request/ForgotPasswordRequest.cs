using System.ComponentModel.DataAnnotations;

namespace BLL.Models.Request
{
    public class ForgotPasswordRequest
    {
        [Required]
        public string ResetCode { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword", ErrorMessage = "Mật Khẩu Xác Nhận Không Khớp")]
        public string ConfirmPassword { get; set; }
    }
}
