using Microsoft.AspNetCore.Mvc;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;
using resturant.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace resturant.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "ShoppingCartSession";
        private readonly IMenuItemsServices _menuItemServices;
        private readonly IOrderServices _orderServices;

        public CartController(IMenuItemsServices menuItemServices, IOrderServices orderServices)
        {
            _menuItemServices = menuItemServices;
            _orderServices = orderServices;
        }

        // 1. عرض صفحة محتويات السلة
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey) ?? new List<CartItemDto>();
            return View(cart);
        }

        // 2. إضافة وجبة إلى السلة
        public IActionResult AddToCart(int id)
        {
            // جلب بيانات الوجبة للتأكد من وجودها وسعرها الحالي
            var item = _menuItemServices.GetMenuItemById(id);
            if (item == null)
            {
                return NotFound();
            }

            // قراءة السلة الحالية من الجلسة أو إنشاء سلة جديدة إن كانت فارغة
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey) ?? new List<CartItemDto>();

            // التحقق مما إذا كانت الوجبة مضافة مسبقاً لزيادة الكمية فقط
            var existingItem = cart.FirstOrDefault(x => x.MenuItemId == id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItemDto
                {
                    MenuItemId = item.id,
                    MenuItemName = item.name,
                    Price = item.price,
                    Quantity = 1,
                    ImageUrl = item.ImageURL
                });
            }

            // حفظ السلة بعد التعديل في الجلسة
            HttpContext.Session.SetObject(CartSessionKey, cart);

            // إعادة التوجيه إلى صفحة السلة
            return RedirectToAction(nameof(Index));
        }
    }
}