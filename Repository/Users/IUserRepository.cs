using JwtPracticePart2.DTO;
using JwtPracticePart2.Model.Entities;

namespace JwtPracticePart2.Repository.Users
{
    public interface IUserRepository
    {
        public Task<Boolean> doesExist(UserPost returnedUser);
        public Task<UserEntity> UserDetails(UserPost returnedUser);
        public Task SaveNewUser(UserEntity userEntity);
        public Task<List<UserEntity>> getUsers();
    }
}