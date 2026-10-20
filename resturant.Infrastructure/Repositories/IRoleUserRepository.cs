
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IRoleUserRepository : IRepository<RoleUser>
    {

        public IEnumerable<int> GetSelectedRoles(int id );

        public IEnumerable<RoleUser> GetRolesByUserId(int userId);
        public void RemoveRange(IEnumerable<RoleUser> userRoles);
    }
}
