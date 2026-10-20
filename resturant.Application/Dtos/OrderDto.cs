using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resturant.Application.Dtos
{
    internal class OrderDto
    {
    }

    

    public class OrderDetailsDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; } = string.Empty;

        public List<CartItemDto> Items { get; set; } = new List<CartItemDto>();
    }
}
