using System.ComponentModel.DataAnnotations;

namespace BLL.Model.ModelRequest
{
    public class ReqForgotPassword
    {
        [Required]
        public string ResetCode { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "new password and confirm does not match")]
        public string ConfirmPassword { get; set; }
    }
}
