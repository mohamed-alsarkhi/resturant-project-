using resturant.Data;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
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