using AutoMapper;
using Azure.Core;
using BLL.IService;
using BLL.Models.Authentication;
using BLL.Models.Request;
using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NHibernate.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BLL.Service
{
    public class AccountCustomer : IAccountCustomer
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly DataContext _dbContext;
      

        public AccountCustomer(UserManager<Customer> userManager, IConfiguration configuration, IMapper mapper, DataContext dbContext)
        {
            _userManager = userManager;
            _configuration = configuration;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        //public async Task<(int, string)> SignUpAsync(SignUpModel model)
        //{
        //    var user = _mapper.Map<Customer>(model);
        //    var checkGmail = await _userManager.FindByEmailAsync(user.Email);
        //    if (checkGmail != null)
        //    {
        //        return (0, "Gmail already exists");
        //    }
        //    var createUser = await _userManager.CreateAsync(user, model.Password);

        //    if (!createUser.Succeeded)
        //    {
        //        return (0, "User creation failed! Please check user details and try again.");
        //    }
        //    var addToRoleResult = await _userManager.AddToRoleAsync(user, "Customer");

        //    if (!addToRoleResult.Succeeded)
        //    {
        //        return (0, "User created, but role assignment failed.");
        //    }
        //    else
        //    {
        //        return (1, "User created and assigned to the 'customer' role successfully!");
        //    }
        //}
        public async Task<(int, string)> SignUpAsync(SignUpModel model)
        {
            if (model == null)
            {
                return (0, "Invalid input");
            }

            var user = _mapper.Map<Customer>(model);
            var checkGmail = await _userManager.FindByEmailAsync(user.Email);
            if (checkGmail != null)
            {
                return (0, "Gmail already exists");
            }

            var createUser = await _userManager.CreateAsync(user, model.Password);

            if (!createUser.Succeeded)
            {
                return (0, "User creation failed! Please check user details and try again.");
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, "Customer");

            if (!addToRoleResult.Succeeded)
            {
                // Rollback user creation if adding role fails
                await _userManager.DeleteAsync(user);
                return (0, "User created, but role assignment failed.");
            }
            else
            {
                return (1, "User created and assigned to the 'customer' role successfully!");
            }
        }


        public async Task<ViewTokenModel> SignInAsync(SignInModel model)
        {
            ViewTokenModel viewTokenModel = new();
            var result = await _userManager.FindByNameAsync(model.Email);
            if (result == null)
            {
                viewTokenModel.StatusCode = 0;
                viewTokenModel.StatusMessage = "Invalid gmail";
                return viewTokenModel;
            }
            if (!await _userManager.CheckPasswordAsync(result, model.Password))
            {
                viewTokenModel.StatusCode = 0;
                viewTokenModel.StatusMessage = "Invalid password";
                return viewTokenModel;
            }
            var userRole = await _userManager.GetRolesAsync(result);
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, model.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            foreach (var user in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, user));
            }

            viewTokenModel.StatusCode = 1;
            viewTokenModel.StatusMessage = "Success";
            viewTokenModel.AccessToken = GenerateToken(authClaims);
            viewTokenModel.RefreshToken = GenerateRefreshToken();
            viewTokenModel.UserID = result.Id;
            viewTokenModel.Username = result.LastName + " " + result.FirstName;

            var _RefreshTokenValidityInDays = Convert.ToInt64(_configuration["JWT:RefreshTokenValidityInDays"]);
            result.RefreshToken = viewTokenModel.RefreshToken;
            result.RefreshTokenExpiryTime = DateTime.Now.AddDays(_RefreshTokenValidityInDays);
            await _userManager.UpdateAsync(result);

            return viewTokenModel;

        }
        public async Task<UpdateUserInfoRequest> GetCustomerByEmailAsync(string email)
        {
            var customer = await _userManager.FindByEmailAsync(email);
            if (customer == null)
            {
                return null;
            }

            // Chuyển đổi đối tượng cơ sở dữ liệu thành DTO (Data Transfer Object)
            return new UpdateUserInfoRequest
            {
                Email = customer.Email,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Birthday = customer.Birthday,
                Gender = customer.Gender,
                Location = customer.Location,
                Image = customer.Image
            };
        }
        public async Task<(int, string)> UpdateUserInfoAsync(UpdateUserInfoRequest updateRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(updateRequest.Email);
                    if (user == null)
                {
                    return (0, "User does not exist");
                }

                user.FirstName = updateRequest.FirstName;
                user.LastName = updateRequest.LastName;
                user.Birthday = updateRequest.Birthday;
                user.Gender = updateRequest.Gender;
                user.Location = updateRequest.Location;
                user.Image = updateRequest.Image;

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return (1, "Update successful");
                }
                else
                {
                    // You might want to log details of why the update failed.
                    return (0, "Update failed");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                
                return (-1, $"An error occurred: {ex.Message}");
            }
        }


        public async Task<ViewTokenModel> GetRefreshToken(GetRefreshTokenView model)
        {
            ViewTokenModel _ViewTokenModel = new();
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            string username = principal.Identity.Name;
            var user = await _userManager.FindByEmailAsync(username);

            if (user == null || user.RefreshToken != model.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                _ViewTokenModel.StatusCode = 0;
                _ViewTokenModel.StatusMessage = "Invalid access token or refresh token";
                return _ViewTokenModel;
            }
            var userRole = await _userManager.GetRolesAsync(user);
            var authClaims = new List<Claim>
            {
               new Claim(ClaimTypes.Name, user.Email),
               new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            foreach (var item in userRole)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, item));
            }

            user.RefreshToken = GenerateRefreshToken();
            await _userManager.UpdateAsync(user);

            _ViewTokenModel.StatusCode = 1;
            _ViewTokenModel.StatusMessage = "Success";
            _ViewTokenModel.AccessToken = GenerateToken(authClaims);
            _ViewTokenModel.RefreshToken = GenerateRefreshToken();
            _ViewTokenModel.UserID = user.Id;
            _ViewTokenModel.Username = user.LastName + " " + user.FirstName;
            return _ViewTokenModel;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }

        private string GenerateToken(IEnumerable<Claim> claims)
        {
            var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:Issuer"],
                Audience = _configuration["JWT:Audience"],
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var random = new Byte[64];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }

        public async Task<int> CheckEmail(string email)
        {
            var checkGmail = await _userManager.FindByEmailAsync(email);
            if (checkGmail != null)
            {
                return 0;
            }
            return 1;
        }

        public async Task<int> SendCodeForgot(string email)
        {
            var checkGmail = await _userManager.FindByEmailAsync(email);
            if (checkGmail == null)
            {
                return 0;
            }
            var rd = new Random();
            int codeForgot = rd.Next(10000, 99999);
            checkGmail.CodeForgotPassword = codeForgot.ToString();
            await _userManager.UpdateAsync(checkGmail);
            return codeForgot;
        }

        public async Task<int> GetNewPassword(ForgotPasswordRequest reqForgotPass)
        {
            var req = _dbContext.Users.Where(x => x.CodeForgotPassword == reqForgotPass.ResetCode).FirstOrDefault();
            if (req != null)
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(req);
                var result = await _userManager.ResetPasswordAsync(req, resetToken, reqForgotPass.NewPassword);
                if (result.Succeeded)
                {
                    req.CodeForgotPassword = "";
                    await _userManager.UpdateAsync(req);
                    return 1;
                }
                return 0;
            }
            return 0;
        }
    }
}
