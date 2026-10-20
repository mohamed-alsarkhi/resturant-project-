using resturant.Infrastructure.Data;

using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        

        public User GetUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u=>u.Username== username);
        }


        public User GetUserByEmail(string email) {

            return _db.Users.FirstOrDefault(u => u.Email == email);
        
        }
    }
}


