
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {

       

        Role? GetRoleWithPermissions(int roleId);
    }
}
