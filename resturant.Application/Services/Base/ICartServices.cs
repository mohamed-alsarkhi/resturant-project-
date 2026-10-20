using resturant.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resturant.Application.Services.Base
{
    public interface ICartServices
    {

        IEnumerable<CartItemDto> GetCartByUserId(int userId);
        bool AddToCart(AddToCartDto dto);
        bool RemoveFromCart(int userId, int menuItemId);
        void ClearCart(int userId);

    }
}
