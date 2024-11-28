using Application.Features.ThongKeFeatures;
using Application.Interface;
using Application.Services;
using Asp.Versioning;
using Domain.Entities;
using Domain.Interfaces;
using Domain.DTO;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Identity.Data;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IUserService _userService;
        protected IApplicationDbContext _context;

        public AuthController(IUserService userService, IApplicationDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var user = await _userService.ValidateUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            else
            {
                user.RememberMe = model.RememberMe; 
                _context.Users.Update(user);
                await _context.SaveChangesAsync(); 

                var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
            }, "Cookies");

                var principal = new ClaimsPrincipal(identity);

                if (model.RememberMe)
                {
                    await HttpContext.SignInAsync("Cookies", principal, new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTime.UtcNow.AddDays(30)
                    });
                }
                else
                {
                    await HttpContext.SignInAsync("Cookies", principal);
                }
                var userDto = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Name = user.Name
                };
                return Ok(new RegisterResponseDTO
                {
                    Success = true,
                    Message = "Login successful",
                    data = userDto
                });
            }          
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO model)
        {
            if (string.IsNullOrEmpty(model.Name) || !Regex.IsMatch(model.Name, @"^[A-Za-zàáãạảăắằẳẵặâấầẩẫậèéẹẻẽêềếểễệđìíĩỉịòóõọỏôốồổỗộơớờởỡợùúũụủưứừửữựỳỵỷỹýÀÁÃẠẢĂẮẰẲẴẶÂẤẦẨẪẬÈÉẸẺẼÊỀẾỂỄỆĐÌÍĨỈỊÒÓÕỌỎÔỐỒỔỖỘƠỚỜỞỠỢÙÚŨỤỦƯỨỪỬỮỰỲỴỶỸÝ\s]+$"))
            {
                return BadRequest(new { message = "Name can only contain letters" });
            }
            if (string.IsNullOrEmpty(model.Username) || !Regex.IsMatch(model.Username, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return BadRequest(new { message = "Invalid Email" });
            }
            if (string.IsNullOrEmpty(model.Password) || model.Password.Length < 6)
            {
                return BadRequest(new { message = "Password must be at least 8 characters long" });
            }
            var user = await _userService.RegisterUserAsync(model.Username, model.Password, model.Name);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name
            };

            return Ok(new RegisterResponseDTO
            {
                Success = true,
                Message = "Registration successful",
                data = userDto
            });
        }

        [HttpPost("update-password")]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userService.UpdatePasswordAsync(model.UserId, model.OldPassword, model.NewPassword);
            if (result.Code != 200) return BadRequest(new { message = result.Message });
            return Ok(new { message = "Password updated successfully" });
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("Cookies");
            return Ok(new { message = "Logout successful" });
        }
    }
}
