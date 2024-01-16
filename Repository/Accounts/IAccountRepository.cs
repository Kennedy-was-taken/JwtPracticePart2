using JwtPracticePart2.Model.Entities;

namespace JwtPracticePart2.Repository.Accounts
{
    public interface IAccountRepository{

        public Task CreateAccount(AccountEntity accountEntity);
        public Task<int> getAccountId(string username);
    }
}