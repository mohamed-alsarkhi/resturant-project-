using Microsoft.EntityFrameworkCore;
using resturant.Data;
using resturant.Dtos;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public class RoleRepository : Repository<Role> , IRoleRepository
    {
        private readonly AppDbContext _db;
        public RoleRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public IEnumerable<RoleDto> GetallDto()
        {

            return _db.Roles.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }

        public Role? GetRoleWithPermissions(int roleId)
        {
            return _db.Roles
                .Include(r => r.Permissions)
                .FirstOrDefault(r => r.Id == roleId);
        }

    }
}
