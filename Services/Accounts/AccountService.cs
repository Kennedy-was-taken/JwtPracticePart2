using JwtPracticePart2.Data;
using JwtPracticePart2.DTO;
using JwtPracticePart2.Model;
using JwtPracticePart2.Model.Entities;
using JwtPracticePart2.Repository.Accounts;
using Microsoft.EntityFrameworkCore;

namespace JwtPracticePart2.Services.Accounts
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(DataContext context, IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void CreateAccount(UserEntity newUser)
        {

            var newAccount = new AccountEntity
            {
                UserRefId = newUser.Id
            };

            //saves the account
            _accountRepository.CreateAccount(newAccount);

        }

        public int getAccountId(string username)
        {
            var userAccount = _accountRepository.getAccountId(username).Result;

            return userAccount;
        }

    }
}