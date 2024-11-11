using Domain.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Application.Interface;
using BCrypt.Net;

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

        public async Task<User> RegisterUserAsync(string username, string password)
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
    }
}
