using System.ComponentModel.DataAnnotations.Schema;

namespace resturant.Models
{
    public class MenuItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string? description { get; set; }
        public bool enabled { get; set; } = true;

        [ForeignKey("Category")]
        public int categoryid { get; set; }
        public Category? Category { get; set; }
    }
}
