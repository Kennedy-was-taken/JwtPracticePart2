using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;
using JwtPracticePart2.Model.Entities;

namespace JwtPracticePart2.Services.Accounts
{
    public interface IAccountService
    {
        public int getAccountId(string username);
        public void CreateAccount(UserEntity newUser);
    }
}