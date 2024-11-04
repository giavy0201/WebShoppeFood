using BLL.Models.Authentication;
using BLL.Models.Request;

namespace BLL.IService
{
    public interface IAccountCustomer
    {
        Task<(int, string)> SignUpAsync(SignUpModel model);
        Task<ViewTokenModel> SignInAsync(SignInModel model);
        Task<UpdateUserInfoRequest> GetCustomerByEmailAsync(string email);
        Task<(int,string)> UpdateUserInfoAsync(UpdateUserInfoRequest updateRequest);
        Task<ViewTokenModel> GetRefreshToken(GetRefreshTokenView model);
        Task<int> CheckEmail(string email);
        Task<int> SendCodeForgot(string email);
        Task<int> GetNewPassword(ForgotPasswordRequest reqForgotPass);
       
    }
}
