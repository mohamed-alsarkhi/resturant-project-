using resturant.Data;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public class RoleUserRepository : Repository<RoleUser>, IRoleUserRepository
    {

        private readonly AppDbContext _db;
        public RoleUserRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public IEnumerable<int> GetSelectedRoles(int id) { 
            return _db.RoleUsers
                .Where(x => x.UserId == id)
                .Select(x => x.RoleId)
                .ToList();
        }

        public IEnumerable<RoleUser> GetRolesByUserId(int userId)
        {
            return _db.RoleUsers
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public void RemoveRange(IEnumerable<RoleUser> userRoles)
        {
            _db.RoleUsers.RemoveRange(userRoles);
        }
    }
}
                                                                                                                                        