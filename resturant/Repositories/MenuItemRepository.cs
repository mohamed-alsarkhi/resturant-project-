using Microsoft.EntityFrameworkCore;
using resturant.Data;
using resturant.Dtos;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public class MenuItemRepository : Repository<MenuItem> , IMenuItemRepository
    {
        private readonly AppDbContext _db;
        public MenuItemRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public IEnumerable<MenuItemDto> GetAllWithCategory()
        {

            return _db.Menu.Select(x => new MenuItemDto 
            {
                id = x.id,
                name = x.name,
                price = x.price,
                description = x.description,
                enabled = x.enabled,
                CategoryName = x.Category.Name

            }).ToList();
        }

        //public IEnumerable<MenuItem> GetAllWithCategory() {

        //    return _db.Menu.Include(x => x.Category).ToList();
        //}
    }
}
