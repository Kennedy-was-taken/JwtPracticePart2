using System.Security.Cryptography;
using JwtPracticePart2.Repository.Tokens;
using JwtPracticePart2.Services.Accounts;

namespace JwtPracticePart2.Services.Tokens
{
    public class TokenService : ITokenService
    {
        
        private readonly IAccountService _accountService;
        private readonly ITokenRepository _tokenRepository;

        public TokenService(IAccountService accountService, ITokenRepository tokenRepository)
        {
            _accountService = accountService;
            _tokenRepository = tokenRepository;
        }


        public string GenerateRefreshToken()
        {
            byte[] encrypted = new byte[32];
            using(var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(encrypted);
                return Convert.ToBase64String(encrypted);
            }
        }

        
        //checks to see if the refresh token matches the one stored in the database
        public Boolean MatchingRefreshToken(string refreshToken, string username){

            int accountRefId = _accountService.getAccountId(username);
            return _tokenRepository.MatchingRefreshToken(refreshToken, accountRefId).Result;
        }

    }
}