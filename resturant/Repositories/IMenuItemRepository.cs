using resturant.Dtos;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        IEnumerable<MenuItemDto> GetAllWithCategory();
    }
}
