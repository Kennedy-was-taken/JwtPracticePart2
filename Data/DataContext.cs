using JwtPracticePart2.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace JwtPracticePart2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<UserEntity> Users {get; set;}
        public DbSet<AccountEntity> Accounts {get; set;}
        public DbSet<TokenEntity> Tokens {get; set;}

    }
}