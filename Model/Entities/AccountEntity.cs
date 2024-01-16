using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtPracticePart2.Model.Entities
{
    [Table("Accounts")]
    public class AccountEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("account_id")]
        public int Id {get; set;}

        //one-to-one relationship with UserEntity
        [ForeignKey("User")]
        [Column("User_id")]
        public int UserRefId {get; set;}
        public virtual UserEntity User {get; set;}

        //one-to-one relationship with TokenEntity
        public virtual TokenEntity Token {get; set;}
    }
}