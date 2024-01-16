using System.Security.Claims;
using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;
using JwtPracticePart2.Model.Entities;

namespace JwtPracticePart2.Services.JwtTokens
{
    public interface IJwtTokenService
    {
        public string GenerateAccessToken(UserPost users);
        public string GenerateAccessTokenWithClaims(IEnumerable<Claim> claims);
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        public Boolean isTokenExpired(string token);
    }
}