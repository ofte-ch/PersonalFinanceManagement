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
using Newtonsoft.Json.Linq;

namespace WebAPIs.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthController(IUserService userService, IConfiguration configuration,ITokenService tokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var user = await _userService.ValidateUserAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            var token = GenerateJwtToken(user, int.Parse(_configuration["Jwt:ExpiresInMinutes"]));
            var refreshToken = GenerateJwtToken(user, int.Parse(_configuration["Jwt:Refresh-token-expiration"]));
            var tokenEtt = new Token
            {
                TokenValue = token,
                Expired = false,
                Revoked = false,
                User = user,
            };
            await revokeAllUserTokens(user);
            await _tokenService.CreateTokenAsync(tokenEtt);

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name
            };
            var response = new
            {
                access_token = token,
                refresh_token = refreshToken,
                user = userDto
            };  
            return Ok(new
            {
                code = 200,
                message = "Login successfully !",
                data = response
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

            var token = GenerateJwtToken(user, int.Parse(_configuration["Jwt:ExpiresInMinutes"]));

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

        [HttpPost("update-user")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequestDTO model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _userService.UpdatePasswordAsync(model.UserId,model.Name, model.OldPassword, model.NewPassword);
            if (result.Code != 200) return BadRequest(new { message = result.Message });
            return Ok(new { message = "User updated successfully" });
        }

        private string GenerateJwtToken(User user,int expiration)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim("UserId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            int expiresInMinutes = expiration;

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(expiresInMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        private string GetUsernameFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

            try
            {
                var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _configuration["Jwt:Audience"],
                    ValidateLifetime = true
                }, out SecurityToken validatedToken);

                // Trích xuất claim chứa username
                var usernameClaim = claimsPrincipal.FindFirst(ClaimTypes.Name);
                return usernameClaim?.Value; // Trả về username hoặc null nếu không tìm thấy
            }
            catch (Exception ex)
            {
                // Xử lý lỗi hoặc trả về giá trị mặc định
                Console.WriteLine("Error extracting username: " + ex.Message);
                return null;
            }
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Missing or invalid Authorization header. Bearer token required.",
                    data = (string)null
                });
            }

            var refreshToken = authHeader.Substring(7);
            var userName = GetUsernameFromToken(refreshToken);
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "Invalid refresh token",
                    data = (string)null
                });
            }

            var user = await _userService.GetUserByUserName(userName);
            if (user == null)
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "User not found",
                    data = (string)null
                });
            }
            await revokeAllUserTokens(user);
            return Ok(new { message = "Logout successful. Please clear the token on the client side." });
        }
        private async void saveUserToken(User user,String jwtToken)
        {
            var token = new Token
            {
                User = user,
                TokenValue = jwtToken,
                Expired = false,
                Revoked = false
            };
            await _tokenService.CreateTokenAsync(token);
        }
        private async Task<bool> revokeAllUserTokens (User user) {
            var validUserToken = await _tokenService.GetTokensByUserIdAsync(user.Id);
            if (validUserToken != null) {
                foreach (var token in validUserToken) {
                    token.Expired = true;
                    token.Revoked = true;
                    await _tokenService.UpdateTokenAsync(token.Id, token);
                }
                return true;
            }
            return false;
        }
        private bool isTokenValid(string token,User user)
        {
            string username = GetUsernameFromToken(token);
            return username.Equals(user.Username);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Missing or invalid Authorization header. Bearer token required.",
                    data = (string)null
                });
            }

            var refreshToken = authHeader.Substring(7);
            var userName = GetUsernameFromToken(refreshToken);
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "Invalid refresh token",
                    data = (string)null
                });
            }

            var user = await _userService.GetUserByUserName(userName);
            if (user == null)
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "User not found",
                    data = (string)null
                });
            }

            if (!isTokenValid(refreshToken, user))
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "Refresh token is not valid",
                    data = (string)null
                });
            }

            var newAccessToken = GenerateJwtToken(user, int.Parse(_configuration["Jwt:ExpiresInMinutes"]));
            await revokeAllUserTokens(user);
            saveUserToken(user, newAccessToken);

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name
            };

            var response = new
            {
                accessToken = newAccessToken,
                refreshToken,
                user = userDto
            };

            return Ok(new
            {
                code = 200,
                message = "Token refreshed successfully",
                data = response
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetMe()
        {
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
            {
                return BadRequest(new
                {
                    code = 400,
                    message = "Missing or invalid Authorization header. Bearer token required.",
                    data = (string)null
                });
            }

            var accessToken = authHeader.Substring(7);
            var userName = GetUsernameFromToken(accessToken);
            if (string.IsNullOrEmpty(userName))
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "Invalid access token",
                    data = (string)null
                });
            }

            var user = await _userService.GetUserByUserName(userName);
            if (user == null)
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "User not found",
                    data = (string)null
                });
            }

            if (!isTokenValid(accessToken, user))
            {
                return Unauthorized(new
                {
                    code = 400,
                    message = "Access token is not valid",
                    data = (string)null
                });
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Name = user.Name
            };
            var response = new
            {
                access_token = accessToken,
                refresh_token = string.Empty,
                user = userDto
            };

            return Ok(new
            {
                code = 200,
                message = "Get information successfully",
                data = response
            });
        }

    }
}
