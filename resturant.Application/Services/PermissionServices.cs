using resturant.Application.Dtos;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;
using resturant.Application.Services.Base;

namespace resturant.Application.Services
{
    public class PermissionServices : IPermissionServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PermissionDto> GetAllPermissions()
        {
            var permissions = _unitOfWork.PermissionRepo.GetAll();

            var permissionsDto = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name
            }).ToList();

            return permissionsDto;
        }

        public void CreatePermission(CreatePermissionDto permissionDto)
        {
            var permission = new Permission
            {
                Name = permissionDto.Name
            };

            _unitOfWork.PermissionRepo.Add(permission);
            _unitOfWork.Save();
        }

        public void UpdatePermission(UpdatePermissionDto permissionDto)
        {
            var permission = new Permission
            {
                Id = permissionDto.Id,
                Name = permissionDto.Name
            };

            _unitOfWork.PermissionRepo.Update(permission);
            _unitOfWork.Save();
        }

        public void DeletePermission(int id)
        {
            var permission = _unitOfWork.PermissionRepo.GetById(id);
            if (permission != null)
            {
                _unitOfWork.PermissionRepo.Delete(permission);
                _unitOfWork.Save();
            }
        }
    }
}