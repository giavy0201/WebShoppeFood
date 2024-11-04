using BLL.Model.Customer;
using BLL.Model.ModelRequest;

namespace BLL.IService
{
    public interface ICustomerService
    {
        Task<int> CheckGmail(string email);
        Task<(int, string)> Register(ModelRegister dbRegister);
        Task<CustomerDtos> SignUp(ModelLogin dbLogin);
        Task<int> ForgotPassword(ReqForgotPassword reqForgotPassword);
        Task<int> SendCodeForgotToEmail(string Email);
        Task<(int, string)> UpdateCustomer(UpdateCustomerRequest updateRequest,string Email);
        Task<UpdateCustomerRequest> GetCustomerByEmailAsync(string email);
    }
}
