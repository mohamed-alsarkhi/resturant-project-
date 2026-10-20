
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IUserFileRepository : IRepository<UserFile>
    {
        
        IEnumerable<UserFile> GetFilesByUserId(int userId);
    }
}