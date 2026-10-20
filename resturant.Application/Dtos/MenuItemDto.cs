using Microsoft.AspNetCore.Http;

namespace resturant.Application.Dtos
{
    public class MenuItemDto
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }
        public string? description { get; set; }
        public bool enabled { get; set; } = true;
        public string? ImageURL { get; set; }

        public string CategoryName { get; set; }
    }

    public class CreateMenuItemDto
    {
        public string name { get; set; }
        public int price { get; set; }
        public string? description { get; set; }
        public bool enabled { get; set; } = true;
        public int categoryid { get; set; }
        public string? ImageURL { get; set; }
        public IFormFile? image { get; set; }
    }

    public class UpdateMenuItemDto : CreateMenuItemDto
    {
        public int id { get; set; }
    }

}
