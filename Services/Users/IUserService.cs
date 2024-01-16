using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;

namespace JwtPracticePart2.Services.Users
{
    public interface IUserService
    {
        public ServiceResponseModel<TokenPost> Login(UserPost returnedUser);
        public ServiceResponseModel<UserPost> Register(UserPost newUser);
        public List<UserPost> users();
    }
}