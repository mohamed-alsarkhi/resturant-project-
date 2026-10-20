using resturant.Application.Dtos;
using resturant.Application.Services.Base;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resturant.Application.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // إنشاء الفاتورة من عناصر السلة وحفظها في قاعدة البيانات
        public int CreateOrder(List<CartItemDto> cartItems, int userId)
        {
            if (cartItems == null || !cartItems.Any())
                return 0;

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                Status = "مكتمل",
                TotalAmount = cartItems.Sum(x => x.TotalPrice),
                OrderItems = cartItems.Select(item => new OrderItem
                {
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                }).ToList()
            };

            _unitOfWork.OrderRepo.Add(order);
            _unitOfWork.Save();

            return order.Id;
        }

        // جلب جميع الطلبات السابقة للمستخدم
        public IEnumerable<OrderDetailsDto> GetUserOrders(int userId)
        {
            var orders = _unitOfWork.OrderRepo.GetOrdersByUserId(userId);

            return orders.Select(o => new OrderDetailsDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status,
                Items = o.OrderItems.Select(oi => new CartItemDto
                {
                    MenuItemId = oi.MenuItemId,
                    MenuItemName = oi.MenuItem?.name ?? string.Empty,
                    Price = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    ImageUrl = oi.MenuItem?.ImageURL
                }).ToList()
            }).ToList();
        }

        // جلب تفاصيل فاتورة واحدة محددة
        public OrderDetailsDto? GetOrderById(int orderId)
        {
            var order = _unitOfWork.OrderRepo.GetOrderWithDetails(orderId);
            if (order == null) return null;

            return new OrderDetailsDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                Items = order.OrderItems.Select(oi => new CartItemDto
                {
                    MenuItemId = oi.MenuItemId,
                    MenuItemName = oi.MenuItem?.name ?? string.Empty,
                    Price = oi.UnitPrice,
                    Quantity = oi.Quantity,
                    ImageUrl = oi.MenuItem?.ImageURL
                }).ToList()
            };
        }
    }
}
