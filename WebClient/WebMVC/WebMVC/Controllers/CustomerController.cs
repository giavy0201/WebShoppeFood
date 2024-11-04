
using BLL.IService;
using BLL.Model.Customer;
using BLL.Model.ModelRequest;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace WebMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly ICustomerService _customerService;
        private readonly IConfiguration _configuration;
        public CustomerController(IHttpContextAccessor context, ICustomerService customerService, IConfiguration configuration)
        {
            _context = context;
            _customerService = customerService;
            _configuration = configuration;
        }
        /// <summary>
        /// UI Register
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(IFormCollection collection, ModelRegister db)
        {
            db.Email = collection["register-email"];
            db.Password = collection["register-password"];
            db.ConfirmPassword = collection["confirm-password"];
            db.FirstName = collection["first-name"];
            db.LastName = collection["last-name"];
            db.Birthday = DateTime.Parse(collection["birthday"]);
            db.Gender = int.Parse(collection["gender"]); // Cần xử lý chuyển đổi chuỗi sang số ở đây
            db.Location = collection["location"];
            db.PhoneNumber = collection["phone-number"];
            db.Image = collection["image"];

            // Kiểm tra hợp lệ của mô hình đăng ký
            if (!ModelState.IsValid)
            {
                // Trả về trang đăng ký với thông báo lỗi nếu mô hình không hợp lệ
                return View(db);
            }

            // Gọi phương thức đăng ký từ service
            var request = await _customerService.Register(db);

            if (request.Item1 == 0)
            {
                ViewBag.Error = "Đăng ký không thành công. Vui lòng thử lại.";
                return View();
            }
            else
            {
                // Nếu đăng ký thành công, bạn có thể thực hiện các hành động khác ở đây, chẳng hạn như lưu thông tin người dùng vào session.
                // Ví dụ:
                _context.HttpContext.Session.SetString("customer", db.Email); // Sử dụng email thay vì Username
                _context.HttpContext.Session.SetString("customerID", request.Item2); // Sử dụng ID từ response

                return RedirectToAction("Index", "Home");
            }
        }





        /// <summary>
        /// UI Login
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(IFormCollection collection, ModelLogin db)
        {
            db.Email = collection["login-name"];
            db.Password = collection["login-password"];
            var request = await _customerService.SignUp(db);
            
            if (request == null || request.StatusCode != 1)
            {
                ViewBag.Error = "Sai Mật Khẩu Hoặc Tài Khoản";
                return View();
            }

            else
            {
                _context.HttpContext.Session.SetString("customer", request.Username);
                _context.HttpContext.Session.SetString("customerID", request.UserID);
                _context.HttpContext.Session.SetString("customerEmail", db.Email);
                
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// logout
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            _context.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// send code gmail
        /// </summary>
        /// <param name="Email"></param>
        /// <param name="Code"></param>
        private void SendVerification(string Email, string Code)
        {
            var fromEmail = new MailAddress("ntd.spf.s3@gmail.com", "Shoppe Food");
            var toEmail = new MailAddress(Email);
            var fromPass = "dqszcghhsszzswni";
            //var localhost = _configuration["https:localhost"] + "Account/ForgotPassword/Change/?code=" + Code;
            var baseUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host;

            var changePasswordUrl = $"{baseUrl}/Account/ForgotPassword/Change/?code={Code}";

            string sub = "Password Reset";
            //string body = $"<p>click link change password: <a href=\"{localhost}\">Reset Password</a></p>";
            string body = $"<p>Click link to change password: <a href=\"{changePasswordUrl}\">Reset Password</a></p>";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromEmail.Address, fromPass)
            };

            using (var mess = new MailMessage(fromEmail, toEmail)
            {
                Subject = sub,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(mess);
            }
        }

        /// <summary>
        /// UI Forgot Password
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Account/Forgotpassword")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet("/Account/ForgotPassword/Change/")]
        public ActionResult ChangeForgotPass(int code)
        {
            ViewBag.CodeForgot = code;
            return View("ChangeForgotPass");
        }
        [HttpPost("/Account/ForgotPassword/Change/")]
        public async Task<ActionResult> ChangeForgotPass(IFormCollection collection, ReqForgotPassword reqForgotPassword)
        {
            reqForgotPassword.ResetCode = collection["code-password"];
            reqForgotPassword.NewPassword = collection["new-password"];
            reqForgotPassword.ConfirmPassword = collection["confirm-password"];
            var request = await _customerService.ForgotPassword(reqForgotPassword);
            if (request == 0)
            {
                ViewBag.Error = "Đổi Mật Khẩu Thất Bại , Xem Lại Thông Tin Nhập";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Customer");
            }
        }

        /// <summary>
        /// send code to gmail
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        //public async Task<IActionResult> SendCodeForgot(string Email)
        //{
        //    //var codeFogot = await _customerService.SendCodeForgotToEmail(Email);
        //    //if (codeFogot == 0)
        //    //{
        //    //    return 0;
        //    //}
        //    //else
        //    //{
        //    //    SendVerification(Email, codeFogot.ToString());
        //    //    return 1;
        //    //}
        //    var result = await _customerService.SendCodeForgotToEmail(Email);
        //    if (result == 0)
        //    {
        //        return RedirectToAction("ForgotPassword", "Customer"); // Xử lý trường hợp gửi mã không thành công
        //    }
        //    else
        //    {
        //        SendVerification(Email, result.ToString());
        //        return RedirectToAction("ChangeForgotPass", "Customer", new { code = result }); // Gửi mã xác nhận qua email và chuyển hướng đến trang đổi mật khẩu với mã xác nhận
        //    }
        //}
        public async Task<IActionResult> SendCodeForgot(string Email)
        {
            var result = await _customerService.SendCodeForgotToEmail(Email);
            if (result == 0)
            {
                return RedirectToAction("ForgotPassword", "Customer"); // Xử lý trường hợp gửi mã không thành công
            }
            else
            {
                SendVerification(Email, result.ToString());
                return RedirectToAction("ChangeForgotPass", "Customer", new { code = result }); // Chuyển hướng đến trang đổi mật khẩu với mã xác nhận
            }
        }
        [HttpGet("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer()
        {
            var email = _context.HttpContext.Session.GetString("customerEmail");
            if (string.IsNullOrEmpty(email))
            {
                return RedirectToAction("Login", "Customer");
            }

            // Gọi phương thức để lấy thông tin người dùng theo email
            var customer = await _customerService.GetCustomerByEmailAsync(email);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            var model = new UpdateCustomerRequest
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Birthday = customer.Birthday,
                Gender = customer.Gender,
                Location = customer.Location,
                Image = customer.Image
            };

            return View(model);
        }

        //[HttpPut("UpdateCustomer")]
        //public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequest updateCustomerRequest)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(updateCustomerRequest);
        //    }

        //    var email = _context.HttpContext.Session.GetString("customerEmail");
        //    if (string.IsNullOrEmpty(email))
        //    {
        //        return RedirectToAction("Login", "Customer");
        //    }

        //    updateCustomerRequest.Email = email; // Đảm bảo email không bị thay đổi

        //    var (status, message) = await _customerService.UpdateCustomer(updateCustomerRequest, email);

        //    switch (status)
        //    {
        //        case 0:
        //            ViewBag.Message = "Update failed: " + message;
        //            return View(updateCustomerRequest);
        //        case 1:
        //            ViewBag.Message = "Update successful: " + message;
        //            return View(updateCustomerRequest);
        //        default:
        //            ViewBag.Message = "An error occurred: " + message;
        //            return View(updateCustomerRequest);
        //    }
        //}
        [HttpPost("UpdateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerRequest updateCustomerRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid model state" });
            }

            var email = HttpContext.Session.GetString("customerEmail");
            if (string.IsNullOrEmpty(email))
            {
                return Unauthorized(new { error = "Unauthorized" });
            }

            updateCustomerRequest.Email = email; // Ensure the email is not changed

            var (status, message) = await _customerService.UpdateCustomer(updateCustomerRequest, email);

            switch (status)
            {
                case 0:
                    return StatusCode(500, new { error = "Update failed", message });
                case 1:
                    return Ok(new { message = "Update successful", data = updateCustomerRequest });
                default:
                    return StatusCode(500, new { error = "An error occurred", message });
            }
        }


    }
}