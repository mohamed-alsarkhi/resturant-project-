using Microsoft.EntityFrameworkCore;
using resturant.Infrastructure.Data;

using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class RoleRepository : Repository<Role> , IRoleRepository
    {
        private readonly AppDbContext _db;
        public RoleRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

       

        public Role? GetRoleWithPermissions(int roleId)
        {
            return _db.Roles
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Id == roleId);
        }

    }
}
