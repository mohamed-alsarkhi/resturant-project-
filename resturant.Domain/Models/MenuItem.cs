using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace resturant.Domain.Models
{
    public class MenuItem
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string? description { get; set; }
        public bool enabled { get; set; } = true;
        public string? ImageURL { get; set; }
        [NotMapped]
        public IFormFile? image { get; set; }

        [ForeignKey("Category")]
        public int categoryid { get; set; }
        public Category? Category { get; set; }
    }
}
