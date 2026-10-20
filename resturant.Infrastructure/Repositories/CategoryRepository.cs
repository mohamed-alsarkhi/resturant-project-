using resturant.Infrastructure.Data;

using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category> , ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        
    }
}
