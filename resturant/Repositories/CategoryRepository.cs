using resturant.Data;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
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
