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
    public class CartServices : ICartServices
    {


        private readonly IUnitOfWork _unitOfWork;

        public CartServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public bool AddToCart(AddToCartDto dto)
        {
            if (dto.Quantity < 1)
                return false;

            // التأكد من وجود الوجبة أولاً
            if (!_unitOfWork.CartRepo.MenuItemExists(dto.MenuItemId))
                return false;

            var existingItem = _unitOfWork.CartRepo.GetCartItem(dto.UserId, dto.MenuItemId);

            if (existingItem == null)
            {
                var newItem = new CartItem
                {
                    UserId = dto.UserId,
                    MenuItemId = dto.MenuItemId,
                    Quantity = dto.Quantity
                };
                _unitOfWork.CartRepo.Add(newItem);
            }
            else
            {
                existingItem.Quantity += dto.Quantity;
                _unitOfWork.CartRepo.Update(existingItem);
            }

            _unitOfWork.Save();
            return true;
        }

        public void ClearCart(int userId)
        {
            var items = _unitOfWork.CartRepo.GetCartByUserId(userId);
            foreach (var item in items)
            {
                _unitOfWork.CartRepo.Delete(item);
            }
            _unitOfWork.Save();
        }

        public IEnumerable<CartItemDto> GetCartByUserId(int userId)
        {
            var cartItems = _unitOfWork.CartRepo.GetCartByUserId(userId);

            return cartItems.Select(item => new CartItemDto
            {
                Id = item.Id,
                MenuItemId = item.MenuItemId,
                MenuItemName = item.menuItem?.name ?? string.Empty,
                Price = item.menuItem?.price ?? 0,
                ImageUrl = item.menuItem?.ImageURL ?? string.Empty,
                Quantity = item.Quantity
            }).ToList();
        }

        public bool RemoveFromCart(int userId, int menuItemId)
        {
            var item = _unitOfWork.CartRepo.GetCartItem(userId, menuItemId);
            if (item == null)
                return false;

            _unitOfWork.CartRepo.Delete(item);
            _unitOfWork.Save();
            return true;
        }
    }
}
