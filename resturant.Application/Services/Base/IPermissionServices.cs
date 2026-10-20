using resturant.Application.Dtos;

namespace resturant.Application.Services.Base
{
    public interface IPermissionServices
    {
        IEnumerable<PermissionDto> GetAllPermissions();
        void CreatePermission(CreatePermissionDto permissionDto);
        void UpdatePermission(UpdatePermissionDto permissionDto);
        void DeletePermission(int id);
    }
}