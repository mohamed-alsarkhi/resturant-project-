
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IPermissionRepository : IRepository<Permission>
    {

        List<Permission> GetSelectedPermissions(List<int> permissionIds);

       
    }
}
