using JwtPracticePart2.Data;
using JwtPracticePart2.DTO;
using JwtPracticePart2.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtPracticePart2.Repository.Users
{

    public class UserRepository : IUserRepository
    {
        private readonly DataContext _dataContext;

        public UserRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> doesExist(UserPost returnedUser)
        {
            var doesExist = await _dataContext.Users.AnyAsync(x => x.Username == returnedUser.Username && x.Password == returnedUser.Password);
            return doesExist;
        }

        public async Task SaveNewUser(UserEntity userEntity)
        {
            await _dataContext.Users.AddAsync(userEntity);
            _dataContext.SaveChanges();
        }

        public async Task<UserEntity> UserDetails(UserPost returnedUser)
        {
            var userDetails = await _dataContext.Users.FirstOrDefaultAsync(u => u.Username == returnedUser.Username && u.Password == returnedUser.Password);
            return userDetails;
        }

        public async Task<List<UserEntity>> getUsers()
        {
            var listofUsers = await _dataContext.Users.ToListAsync();
            return listofUsers;
        }

    }
}