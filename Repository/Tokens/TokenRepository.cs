using JwtPracticePart2.Data;
using JwtPracticePart2.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtPracticePart2.Repository.Tokens
{
    public class TokenRepository : ITokenRepository
    {
        private readonly DataContext _dataContext;

        public TokenRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> MatchingRefreshToken(string refreshToken, int accountRefId)
        {
            return await _dataContext.Tokens.AnyAsync(t => t.AccountRefId == accountRefId && t.RefreshToken == refreshToken);
        }

        public async Task SaveNewRefreshToken(TokenEntity newToken)
        {
            await _dataContext.Tokens.AddAsync(newToken);
            _dataContext.SaveChanges();
        }

        public async Task<TokenEntity> TokenDetails(int accountRefId)
        {
            var token = await _dataContext.Tokens.FirstOrDefaultAsync(t => t.AccountRefId == accountRefId);
            return token;
        }

        public async Task UpdateRefreshToken(string newRefreshToken, TokenEntity tokenEntity)
        {

            //replacing the retrieved refreshToken with a new RefreshToken
            tokenEntity.RefreshToken = newRefreshToken;
            _dataContext.SaveChanges();
            
        }

    }
}