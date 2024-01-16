using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtPracticePart2.Data;
using JwtPracticePart2.DTO;
using JwtPracticePart2.Repository.Accounts;
using JwtPracticePart2.Repository.Tokens;
using JwtPracticePart2.Services.Accounts;
using JwtPracticePart2.Services.JwtTokens;
using Microsoft.IdentityModel.Tokens;

namespace JwtPracticePart2.Services.Tokens
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAccountRepository _accountRepository;

        public JwtTokenService(IConfiguration configuration, DataContext dataContext, IAccountService accountService, ITokenRepository tokenRepository, IAccountRepository accountRepository)
        {
            _configuration = configuration;
            _accountService = accountService;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
        }


        public string GenerateAccessToken(UserPost users)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key:secretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //creating token properties
            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(new Claim[]{
                    new Claim(ClaimTypes.Name, users.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = signingCredentials          
            };

            //creating token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //converts the token to a string
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }

        public string GenerateAccessTokenWithClaims(IEnumerable<Claim> claims)
        {
        
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key:secretKey"]));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            //creating token properties
            var tokenDescriptor = new JwtSecurityToken(
                claims : claims,
                expires : DateTime.UtcNow.AddMinutes(1),
                signingCredentials : signingCredentials
            );

            var tokenHandler = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return tokenHandler;
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = TokenParameters();

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;

            if(jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)){
                throw new SecurityTokenException("Invalid Token");
            }

            return principal;
        }

        public bool isTokenExpired(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jsonToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            if(jsonToken != null && jsonToken.ValidTo < DateTime.UtcNow){
                return true;
            }

            return false;
        }

        //validation parameters
        private TokenValidationParameters TokenParameters(){

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Key:secretKey"]))
            };

            return tokenValidationParameters;
        }
    }
}