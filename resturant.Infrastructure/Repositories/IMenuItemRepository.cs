
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IMenuItemRepository : IRepository<MenuItem>
    {
        IEnumerable<MenuItem> GetAllWithCategory();
    }
}
