using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {

        List<Permission> GetSelectedPermissions(List<int> permissionIds);
    }
}
