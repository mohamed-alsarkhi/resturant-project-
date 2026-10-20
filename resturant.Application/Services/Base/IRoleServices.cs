using resturant.Application.Dtos;
using resturant.Domain.Models;

namespace resturant.Application.Services.Base
{
    public interface IRoleServices
    {
        IEnumerable<RoleDto> GetAllRoles();
        void CreateRole(CreateRoleDto roleDto);
        void UpdateRole(UpdateRoleDto roleDto);
        void DeleteRole(int id);
        Role? GetRoleWithPermissions(int roleId);
        IEnumerable<Permission> GetAllPermissions();
        void AssignPermissions(int roleId, List<int> permissionIds);
    }
}