using Microsoft.EntityFrameworkCore;
using resturant.Infrastructure.Data;

using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class MenuItemRepository : Repository<MenuItem> , IMenuItemRepository
    {
        private readonly AppDbContext _db;
        public MenuItemRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public IEnumerable<MenuItem> GetAllWithCategory()
        {
            return _db.Menu.Include(x => x.Category).ToList();
        }

        //public IEnumerable<MenuItem> GetAllWithCategory() {

        //    return _db.Menu.Include(x => x.Category).ToList();
        //}
    }
}
