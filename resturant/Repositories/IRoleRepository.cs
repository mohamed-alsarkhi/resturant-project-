using resturant.Dtos;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {

        public IEnumerable<RoleDto> GetallDto();

        Role? GetRoleWithPermissions(int roleId);
    }
}
