using System.ComponentModel.DataAnnotations.Schema;

namespace resturant.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }
        public string? HashPassword { get; set; }

        public string Username { get; set; }

        public string? ImageURL { get; set; }

        [NotMapped]
        public IFormFile? image { get; set; }

        public ICollection<Role> Roles { get; set; } = new List<Role>();
    }
}