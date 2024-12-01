using Asp.Versioning;
using Domain.Entities;
using Domain.Interfaces;
using Domain.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var user = await _userService.ValidateUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = GenerateJwtToken(user);

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name
            };

            return Ok(new LoginResponseDTO
            {
                Success = true,
                Message = "Login successful",
                Token = token,
                data = userDto
            });
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
                return BadRequest(new { message = "Registration failed" });
            }

            var token = GenerateJwtToken(user);

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

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiresInMinutes = int.Parse(_configuration["Jwt:ExpiresInMinutes"]);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            return Ok(new { message = "Logout successful. Please clear the token on the client side." });
        }
    }
}
