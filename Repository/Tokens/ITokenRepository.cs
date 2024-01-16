using JwtPracticePart2.Model.Entities;

namespace JwtPracticePart2.Repository.Tokens
{
    public interface ITokenRepository{
        public Task<TokenEntity> TokenDetails(int accountRefId);
        public Task UpdateRefreshToken(string newRefreshToken, TokenEntity tokenEntity);
        public Task SaveNewRefreshToken(TokenEntity newToken);
        public Task<Boolean> MatchingRefreshToken(string refreshToken, int accountRefId);
    }
}