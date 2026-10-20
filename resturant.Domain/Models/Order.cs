using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace resturant.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalAmount { get; set; }

        // حالة الطلب (مثلاً: Pending, Completed, Cancelled)
        public string Status { get; set; } = "Pending";

        // ربط الطلب بالمستخدم الذي قام بالطلب
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        // قائمة الأصناف المشمولة في هذه الفاتورة
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    }
}
