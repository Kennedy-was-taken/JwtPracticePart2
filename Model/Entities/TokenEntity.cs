using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtPracticePart2.Model.Entities
{
    [Table("Tokens")]
    public class TokenEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("token_id")]
        public int TokenId {get; set;}

        [Column("refresh_token")]
        public string RefreshToken {get; set;}

        //one-to-one relationship
        [ForeignKey("Account")]
        public int AccountRefId {get; set;}
        public virtual AccountEntity Account {get; set;}
    }
}