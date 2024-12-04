using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUserService
    {
        Task<User> ValidateUserAsync(string username, string password);
        Task<User> RegisterUserAsync(string username, string password, string name);
        Task<IResponse> UpdatePasswordAsync(int userId,string name, string oldPassword, string newPassword);
        Task<User?> GetUserByUserName(string userName);
    }

}
