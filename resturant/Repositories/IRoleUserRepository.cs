using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public interface IRoleUserRepository : IRepository<RoleUser>
    {

        public IEnumerable<int> GetSelectedRoles(int id );

        public IEnumerable<RoleUser> GetRolesByUserId(int userId);
        public void RemoveRange(IEnumerable<RoleUser> userRoles);
    }
}
