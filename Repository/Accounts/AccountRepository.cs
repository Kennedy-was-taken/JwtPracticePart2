using JwtPracticePart2.Data;
using JwtPracticePart2.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtPracticePart2.Repository.Accounts
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DataContext _dataContext;

        public AccountRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateAccount(AccountEntity accountEntity)
        {
            await _dataContext.Accounts.AddAsync(accountEntity);
            _dataContext.SaveChanges();
        }

        public async Task<int> getAccountId(string username)
        {
            var userAccount = await _dataContext.Accounts.SingleAsync(c => c.User.Username == username);
            return userAccount.Id;
        }

    }
}