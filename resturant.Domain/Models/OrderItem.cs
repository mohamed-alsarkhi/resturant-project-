using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resturant.Domain.Models
{
    public class OrderItem
    {

        public int Id { get; set; }

        public int Quantity { get; set; }

        // السعر وقت الشراء (لحفظ السعر حتى لو تغير لاحقاً في المنيو)
        public decimal UnitPrice { get; set; }

        // ربط الصنف بالطلب (الفاتورة)
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order? Order { get; set; }

        // ربط الصنف بوجبة الطعام من المنيو
        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
    }
}
