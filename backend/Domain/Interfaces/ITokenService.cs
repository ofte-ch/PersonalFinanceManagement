using Application.Interface;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ITokenService
    {
        Task<Token> CreateTokenAsync(Token token);
        Task<Token?> GetTokenByIdAsync(int id);
        Task<IEnumerable<Token>> GetAllTokensAsync();
        Task<Token> UpdateTokenAsync(int id, Token updatedToken);
        Task<bool> DeleteTokenAsync(int id);

        Task<IEnumerable<Token>> GetTokensByUserIdAsync(int userId);
        Task<Token?> FindByTokenAsync(string token);
    }
}
