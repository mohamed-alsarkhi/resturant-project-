using System.ComponentModel.DataAnnotations.Schema;

namespace resturant.Models
{
    public class RoleUser
    {
        [Column("RolesId")]
        [ForeignKey("Roles")]
        public int RoleId { get; set; }
        public Role Roles { get; set; }

        [Column("UsersId")]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public User Users { get; set; }
    }



}
