using resturant.Data;
using resturant.Dtos;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public class UserRepository : Repository<User> , IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public IEnumerable<UserDto> GetallImprove() {

            return _db.Users.Select(x => new UserDto
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Username = x.Username,
                ImageURL=x.ImageURL
               

            }).ToList();
        }

        public User GetUserByUsername(string username)
        {
            return _db.Users.FirstOrDefault(u=>u.Username== username);
        }
    }
}


