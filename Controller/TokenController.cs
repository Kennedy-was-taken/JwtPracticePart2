using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;
using JwtPracticePart2.Model.Entities;
using JwtPracticePart2.Repository.Tokens;
using JwtPracticePart2.Services.Accounts;
using JwtPracticePart2.Services.JwtTokens;
using JwtPracticePart2.Services.Tokens;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtPracticePart2.Controller
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IAccountService _accountService;

        private readonly ITokenRepository _tokenRepository;
        private readonly ITokenService _tokenService;

        public TokenController(IJwtTokenService jwtTokenService, IAccountService accountService, ITokenRepository tokenRepository, ITokenService tokenService)
        {
            _jwtTokenService = jwtTokenService;
            _accountService = accountService;
            _tokenRepository = tokenRepository;
            _tokenService = tokenService;

        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public ActionResult<ServiceResponseModel<TokenPost>> RefreshToken([FromBody] TokenPost expiredToken){
            ServiceResponseModel<TokenPost> serviceResponse = new ServiceResponseModel<TokenPost>();

            //if expiredToken comes back null return BadRequest
            if(expiredToken.AccessToken == null && expiredToken.RefreshToken == null){
                return BadRequest("Tokens are empty");
            }

            if(!_jwtTokenService.isTokenExpired(expiredToken.AccessToken)){
                return BadRequest("Token still active");
            }

            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(expiredToken.AccessToken);

            //if principal comes back null return BadRequest
            if(principal == null){
                return BadRequest("Invalid Token");
            }

            var isMatching = _tokenService.MatchingRefreshToken(expiredToken.RefreshToken,principal.Identity.Name);

            //if results comes back false return BadRequest
            if(!isMatching){
                return BadRequest("Invalid Token");
            }

            //creating a new TokenPost object
            var newToken = new TokenPost{
                AccessToken = _jwtTokenService.GenerateAccessTokenWithClaims(principal.Claims),
                RefreshToken = _tokenService.GenerateRefreshToken()
            };

            var token = _tokenRepository.TokenDetails(_accountService.getAccountId(principal.Identity.Name)).Result;

            _tokenRepository.UpdateRefreshToken(newToken.RefreshToken, token);

            serviceResponse.Data = newToken;
            serviceResponse.isSuccessful = true;
            serviceResponse.Message = "Tokens Refreshed";

            return Ok(serviceResponse);
        }
    }

}