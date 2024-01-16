namespace JwtPracticePart2.Services.Tokens
{
    public interface ITokenService
    {
        public string GenerateRefreshToken();        
        public Boolean MatchingRefreshToken(string refreshToken, string username);
    }
}