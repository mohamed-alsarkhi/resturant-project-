using resturant.Application.Dtos;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;
using resturant.Application.Services.Base;

namespace resturant.Application.Services
{
    public class RoleServices : IRoleServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoleServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<RoleDto> GetAllRoles()
        {
            var roles = _unitOfWork.RoleRepo.GetAll();

            var rolesDto = roles.Select(r => new RoleDto
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            return rolesDto;
        }

        public void CreateRole(CreateRoleDto roleDto)
        {
            var role = new Role
            {
                Name = roleDto.Name
            };

            _unitOfWork.RoleRepo.Add(role);
            _unitOfWork.Save();
        }

        public void UpdateRole(UpdateRoleDto roleDto)
        {
            var role = new Role
            {
                Id = roleDto.Id,
                Name = roleDto.Name
            };

            _unitOfWork.RoleRepo.Update(role);
            _unitOfWork.Save();
        }

        public void DeleteRole(int id)
        {
            var role = _unitOfWork.RoleRepo.GetById(id);
            if (role != null)
            {
                _unitOfWork.RoleRepo.Delete(role);
                _unitOfWork.Save();
            }
        }

        public Role? GetRoleWithPermissions(int roleId)
        {
            return _unitOfWork.RoleRepo.GetRoleWithPermissions(roleId);
        }

        public IEnumerable<Permission> GetAllPermissions()
        {
            return _unitOfWork.PermissionRepo.GetAll();
        }

        public void AssignPermissions(int roleId, List<int> permissionIds)
        {
            var role = _unitOfWork.RoleRepo.GetRoleWithPermissions(roleId);
            if (role == null) return;

            // حذف الصلاحيات القديمة
            role.Permissions.Clear();

            // جلب الصلاحيات المحددة
            var permissions = _unitOfWork.PermissionRepo.GetSelectedPermissions(permissionIds);

            // إضافة الصلاحيات المختارة
            foreach (var permission in permissions)
            {
                role.Permissions.Add(permission);
            }

            _unitOfWork.Save();
        }
    }
}