using System.ComponentModel.DataAnnotations;

namespace resturant.Application.Dtos
{
    public class PermissionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    
    public class CreatePermissionDto
    {
        
        public string Name { get; set; }
    }

    
    public class UpdatePermissionDto : PermissionDto
    {
    }
}
