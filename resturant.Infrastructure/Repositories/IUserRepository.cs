
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IUserRepository : IRepository<User>
    {

        

        public User GetUserByUsername(string username);


         User GetUserByEmail(string email);
    }
}
