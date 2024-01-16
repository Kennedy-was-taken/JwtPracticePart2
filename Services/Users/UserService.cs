using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;
using JwtPracticePart2.Data;
using Microsoft.EntityFrameworkCore;
using JwtPracticePart2.Services.Tokens;
using JwtPracticePart2.Model.Entities;
using Microsoft.Identity.Client;
using JwtPracticePart2.Services.Accounts;
using JwtPracticePart2.Repository.Users;
using JwtPracticePart2.Repository.Tokens;
using JwtPracticePart2.Repository.Accounts;
using JwtPracticePart2.Services.JwtTokens;

namespace JwtPracticePart2.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ITokenService _tokenService;
        private readonly IAccountService _accountService;
        private readonly IUserRepository _userRepository;
        private readonly ITokenRepository _tokenRepository;
        private readonly IAccountRepository _accountRepository;

        public UserService(DataContext dataContext, ITokenService tokenService, IAccountService accountService, IUserRepository userRepository, ITokenRepository tokenRepository, IAccountRepository accountRepository, IJwtTokenService jwtTokenService)
        {
            _tokenService = tokenService;
            _accountService = accountService;
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
            _accountRepository = accountRepository;
            _jwtTokenService = jwtTokenService;
        }

        public ServiceResponseModel<TokenPost> Login(UserPost returnedUser)
        {
            ServiceResponseModel<TokenPost> serviceResponse = new ServiceResponseModel<TokenPost>(); 
            var doesExist = _userRepository.doesExist(returnedUser).Result;
            if(!doesExist.Equals(false)){

                //extract the users information
                var userDetails = _userRepository.UserDetails(returnedUser).Result;

                //mapping the userinfo into the UserPost object
                var user = mapToUserPost(userDetails);

                //creating a TokenPost object
                var newTokenPost = new TokenPost{
                    AccessToken = _jwtTokenService.GenerateAccessToken(user),
                    RefreshToken = _tokenService.GenerateRefreshToken()
                };

                var token = _tokenRepository.TokenDetails(_accountService.getAccountId(user.Username)).Result;

                //creating a TokenEntity object
                var newToken = new TokenEntity{
                    RefreshToken = newTokenPost.RefreshToken,
                    AccountRefId = _accountService.getAccountId(user.Username)
                };

                if(token != null){
                    
                    _tokenRepository.UpdateRefreshToken(newToken.RefreshToken, token);
                }
                else{

                    _tokenRepository.SaveNewRefreshToken(newToken);
                }

                serviceResponse.Data = newTokenPost;
                serviceResponse.isSuccessful = true;
                serviceResponse.Message = "Successfully logged in";
                
            }
            else{
                serviceResponse.isSuccessful = false;
                serviceResponse.Message = "Username or Password is invalid"; 
            }
            return serviceResponse;
        }

        public ServiceResponseModel<UserPost> Register(UserPost newUser)
        {
            ServiceResponseModel<UserPost> serviceResponse = new ServiceResponseModel<UserPost>();
            var doesExist = _userRepository.doesExist(newUser).Result;

            if(doesExist.Equals(false)){

                var registeringUser = new UserEntity{
                    Username = newUser.Username,
                    Password = newUser.Password
                };

                //saves new user and creates an account
                _userRepository.SaveNewUser(registeringUser);
                _accountService.CreateAccount(registeringUser);

                serviceResponse.Data = mapToUserPost(registeringUser);
                serviceResponse.isSuccessful = true;
                serviceResponse.Message = "Successfully Registered";

            }
            else{
                serviceResponse.isSuccessful = false;
                serviceResponse.Message = "Username already exists"; 
            }

            return serviceResponse;
        }

        private UserPost mapToUserPost(UserEntity user){
            var returnedUser = new UserPost{
                Username = user.Username,
                Password = null
            };

            return returnedUser;
        }

        public List<UserPost> users()
        {
            var listUsers = _userRepository.getUsers().Result;

            List<UserPost> newList = new List<UserPost>();

            foreach(var yes in listUsers){
                newList.Add(mapToUserPost(yes));
            }

            return newList;
        }
    }
}