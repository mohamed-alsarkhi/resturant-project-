using resturant.Models;
using resturant.Repositories.Base;

namespace resturant.Repositories
{
    public interface IUserFileRepository : IRepository<UserFile>
    {
        
        IEnumerable<UserFile> GetFilesByUserId(int userId);
    }
}