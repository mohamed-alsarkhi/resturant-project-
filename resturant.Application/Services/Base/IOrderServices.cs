using resturant.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resturant.Application.Services.Base
{
    public interface IOrderServices
    {
        // حفظ عناصر السلة كطلب جديد في قاعدة البيانات وإرجاع رقم الفاتورة
        int CreateOrder(List<CartItemDto> cartItems, int userId);

        // جلب جميع طلبات وفواتير مستخدم معين
        IEnumerable<OrderDetailsDto> GetUserOrders(int userId);

        // جلب تفاصيل فاتورة واحدة برقمها
        OrderDetailsDto? GetOrderById(int orderId);
    }
}
