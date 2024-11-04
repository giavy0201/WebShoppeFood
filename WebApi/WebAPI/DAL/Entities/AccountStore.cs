using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities
{
    public class AccountStore
    {
        [Key]
        [Column("AccountStoreID")]
        public int Id { get; set; }
        [Required]
        [Column("LoginName")]
        public string LoginName { get; set; }
        [Required]
        [Column("Password")]
        public string Password { get; set; }
        [Required]
        [Column("UserName")]
        public string UserName { get; set; }
        public int StoreID { get; set; }
        [ForeignKey("StoreID")]
        public Store Store { get; set; }
    }
}
