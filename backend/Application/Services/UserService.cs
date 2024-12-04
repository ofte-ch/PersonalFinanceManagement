using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interface;
using BCrypt.Net;
using Application.Response;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class UserService : IUserService
    {
        protected IApplicationDbContext _context;

        public UserService(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> ValidateUserAsync(string username, string password)
        {
            var user = await _context.Users
                                      .Where(u => u.Username == username)
                                      .FirstOrDefaultAsync();
            if (user == null)
            {
                return null; 
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (isPasswordValid)
            {
                return user; 
            }

            return null;

        }

        public async Task<IResponse> UpdatePasswordAsync(int userId,string name, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return new NotFoundResponse("User not found");

            // Kiểm tra mật khẩu cũ
            if (!BCrypt.Net.BCrypt.Verify(oldPassword, user.Password))
                return new BadRequestResponse("Incorrect old password");

            // Hash mật khẩu mới và cập nhật
            user.Password = BCrypt.Net.BCrypt.HashPassword(newPassword);
            user.Name = name;
            await _context.SaveChangesAsync();

            return new SuccessResponse("Password updated successfully");
        }



        public async Task<User> RegisterUserAsync(string username, string password, string name)
        {
            try
            {
                var existingUser = await _context.Users
                                      .Where(u => u.Username == username)
                                      .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    throw new Exception("Username already exists."); 
                }

                
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new User
                {
                    Name = name,
                    Username = username,
                    Password = hashedPassword
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while registering the user.", ex);
            }
        }

        public async Task<User?> GetUserByUserName(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
            return user;
        }
    }
}
