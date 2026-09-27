using System.ComponentModel.DataAnnotations.Schema;

namespace resturant.Models
{
    public class PermissionRole
    {
        [ForeignKey("Permissions")]
        public int PermissionsId { get; set; }
        public Permission Permissions { get; set; } = new Permission();

        [ForeignKey("Roles")]
        public int RolesId { get; set; }
        public Role Roles { get; set; } = new Role();
    }
}
