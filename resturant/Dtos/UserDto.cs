namespace resturant.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string? ImageURL { get; set; }
    }

    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? HashPassword { get; set; }
        public IFormFile? image { get; set; }
    }
    public class UpdateUserDto : CreateUserDto
    {
        public int Id { get; set; }
       
    }
}
