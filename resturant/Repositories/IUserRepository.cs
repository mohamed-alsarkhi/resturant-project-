using resturant.Dtos;
using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        public IEnumerable<UserDto> GetallImprove();

        public User GetUserByUsername(string username);
    }
}
