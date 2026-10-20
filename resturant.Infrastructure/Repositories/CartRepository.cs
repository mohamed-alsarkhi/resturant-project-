using Microsoft.EntityFrameworkCore;
using resturant.Domain.Models;
using resturant.Infrastructure.Data;
using resturant.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace resturant.Infrastructure.Repositories
{
    public class CartRepository : Repository<CartItem>, ICartRepository
    {
        private readonly AppDbContext _db;

        public CartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        // 1. استعلام جلب سلة المستخدم متضمناً الوجبة لعرض الاسم والسعر والصورة
        public IEnumerable<CartItem> GetCartByUserId(int userId)
        {
            return _db.CartItems
                .Include(x => x.menuItem)
                .Where(x => x.UserId == userId)
                .ToList();
        }

        // 2. البحث عن الوجبة داخل سلة المستخدم
        public CartItem GetCartItem(int userId, int menuItemId)
        {
            return _db.CartItems
                .FirstOrDefault(x => x.UserId == userId && x.MenuItemId == menuItemId);
        }

        // 3. التحقق من وجود الوجبة في جدول الوجبات
        public bool MenuItemExists(int menuItemId)
        {
            return _db.Menu.Any(x => x.id == menuItemId);
        }

        // 4. حذف العنصر من السلة
        public void RemoveItemFromCart(int userId, int menuItemId)
        {
            var item = GetCartItem(userId, menuItemId);
            if (item != null)
            {
                _db.CartItems.Remove(item);
            }
        }
    }
}