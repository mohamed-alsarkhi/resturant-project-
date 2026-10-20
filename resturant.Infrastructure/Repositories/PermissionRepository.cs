using resturant.Infrastructure.Data;

using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class PermissionRepository : Repository<Permission>, IPermissionRepository
    {
        private readonly AppDbContext _db;

        public PermissionRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

       

        public List<Permission> GetSelectedPermissions(List<int> permissionIds)
        {
            if (permissionIds == null || !permissionIds.Any())
            {
                return new List<Permission>();
            }

            return _db.Permissions
                .Where(p => permissionIds.Contains(p.Id))
                .ToList();
        }
    }
}
