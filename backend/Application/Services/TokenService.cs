using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace Application.Services
{
    public class TokenService : ITokenService
    {
        protected IApplicationDbContext _context;

        public TokenService(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Token> CreateTokenAsync(Token token)
        {
            try
            {
                await _context.Tokens.AddAsync(token);
                await _context.SaveChangesAsync();
                return token;
            }
            catch (Exception ex) {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Token?> GetTokenByIdAsync(int id)
        {
            return await _context.Tokens.FindAsync(id);
        }

        public async Task<IEnumerable<Token>> GetAllTokensAsync()
        {
            return await _context.Tokens.ToListAsync();
        }

        public async Task<Token> UpdateTokenAsync(int id, Token updatedToken)
        {
            var existingToken = await _context.Tokens.FindAsync(id);
            if (existingToken == null)
                throw new KeyNotFoundException($"Token with ID {id} not found.");

            // Cập nhật các thuộc tính
            existingToken.Expired = updatedToken.Expired;
            existingToken.Revoked = updatedToken.Revoked;
            await _context.SaveChangesAsync();
            return existingToken;
        }

        public async Task<bool> DeleteTokenAsync(int id)
        {
            var token = await _context.Tokens.FindAsync(id);
            if (token == null) return false;

            _context.Tokens.Remove(token);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Token>> GetTokensByUserIdAsync(int userId)
        {
            return await _context.Tokens
                .Where(t => t.User.Id == userId && t.Expired == false) 
                .ToListAsync();
        }
        public async Task<Token?> FindByTokenAsync(string token)
        {
            return await _context.Tokens
                .FirstOrDefaultAsync(t => t.TokenValue == token);
        }

    }
}
