using Microsoft.EntityFrameworkCore;
using resturant.Infrastructure.Data;

using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order> , IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) : base(db)
        {
            _db = db;

        }

        public IEnumerable<Order> GetOrdersByUserId(int userId)
        {
            return _db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.UserId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToList();
        }

        public Order? GetOrderWithDetails(int orderId)
        {
            return _db.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefault(o => o.Id == orderId);
        }

    }
}
