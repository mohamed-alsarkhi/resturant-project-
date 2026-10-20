using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;
using System.Collections.Generic;

namespace resturant.Infrastructure.Repositories
{
    public interface ICartRepository : IRepository<CartItem>
    {
        // 1. جلب محتويات السلة الخاصة بمستخدم معين مع بيانات الوجبة (بدلاً من GetCart)
        IEnumerable<CartItem> GetCartByUserId(int userId);

        // 2. فحص وجلب عنصر معين في سلة مستخدم (يستخدم في الإضافة والتعديل والحذف)
        CartItem GetCartItem(int userId, int menuItemId);

        // 3. التحقق من وجود الوجبة في النظام
        bool MenuItemExists(int menuItemId);

        // 4. دالة حذف عنصر محدد من سلة المستخدم مباشرة
        void RemoveItemFromCart(int userId, int menuItemId);
    }
}