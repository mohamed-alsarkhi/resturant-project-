using resturant.Infrastructure.Data;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class UserFileRepository : Repository<UserFile>, IUserFileRepository
    {
        private readonly AppDbContext _db;

        public UserFileRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<UserFile> GetFilesByUserId(int userId)
        {
            return _db.UserFiles
                .Where(e => e.UserId == userId)
                .ToList();
        }
    }
}