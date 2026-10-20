using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Infrastructure.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {

        // دالة مخصصة لجلب طلبات مستخدم معين متضمنة عناصر الفاتورة والوجبات
        IEnumerable<Order> GetOrdersByUserId(int userId);

        // دالة مخصصة لجلب تفاصيل فاتورة واحدة بكل أصنافها
        Order? GetOrderWithDetails(int orderId);

    }
}
