using BLL.IService;
using BLL.Models.Authentication;
using BLL.Models.Request;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IAccountCustomer _accountRepo;

        public CustomersController(IAccountCustomer accountRepo)
        {
            _accountRepo = accountRepo;
        }
        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel model)
        {
            var (status, message) = await _accountRepo.SignUpAsync(model);
            if (status == 0)
            {
                return BadRequest(message);
            }

            return CreatedAtAction(nameof(SignUp), model);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel model)
        {
            var reslut = await _accountRepo.SignInAsync(model);
            if (reslut == null)
            {
                return Unauthorized();
            }

            return Ok(reslut);
        }
        [HttpGet("GetCustomerByEmail/{email}")]
        public async Task<IActionResult> GetCustomerByEmail(string email)
        {
            var customer = await _accountRepo.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            return Ok(customer);
        }
        [HttpPut("{email}")]
        public async Task<IActionResult> UpdateUserInfo(string email, [FromBody] UpdateUserInfoRequest updateRequest)
        {
            if (updateRequest == null || string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Invalid request body or email");
            }

            if (updateRequest.Email != email)
            {
                return BadRequest("Email mismatch");
            }

            var (status, message) = await _accountRepo.UpdateUserInfoAsync(updateRequest);

            switch (status)
            {
                case 0:
                    return BadRequest(message);
                case 1:
                    return Ok(message);
                default:
                    return StatusCode(500, message);
            }
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(GetRefreshTokenView model)
        {
            if (model is null)
            {
                return BadRequest("Invalid client request");
            }
            var result = await _accountRepo.GetRefreshToken(model);
            if (result.StatusCode == 0)
                return BadRequest(result.StatusMessage);
            return Ok(result);

        }
        [HttpGet("Check-gmail/{email}")]
        public async Task<int> CheckGmail(string email)
        {
            var result = await _accountRepo.CheckEmail(email);
            if (result == 0)
                return 0;
            return 1;

        }

        [HttpGet("ForgotPassword/{email}")]
        public async Task<int> SendCodeGmail(string email)
        {
            var result = await _accountRepo.SendCodeForgot(email);
            if (result == 0)
                return 0;
            return result;

        }

        [HttpPost("ForgotPassword/ChangePassword")]
        public async Task<int> ForgotPassword(ForgotPasswordRequest reqForgotPass)
        {
            var result = await _accountRepo.GetNewPassword(reqForgotPass);
            if (result == 0)
                return 0;
            return result;

        }
    }
}
