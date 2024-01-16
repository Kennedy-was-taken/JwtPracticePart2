using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JwtPracticePart2.Model.Entities
{
    [Table("Users")]
    public class UserEntity
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Column("username")]
        public string Username {get; set;}

        [Column("password")]
        public string Password {get; set;}

        //one-to-one relationship
        public virtual AccountEntity Account {get; set;}
    }
}