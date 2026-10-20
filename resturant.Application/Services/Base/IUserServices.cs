using Microsoft.AspNetCore.Http;
using resturant.Application.Dtos;
using resturant.Domain.Models;

namespace resturant.Application.Services.Base
{
    public interface IUserServices
    {
        IEnumerable<UserDto> GetAllUsers();
        void CreateUser(User user);
        UpdateUserDto? GetForEdit(int id);
        void UpdateUser(UpdateUserDto userDto);
        void DeleteUser(int id);
        UserRolesVM? GetUserRoles(int id);
        void UpdateUserRoles(UserRolesVM model);
        IEnumerable<UserFile> GetUserFiles(int userId);
        string? GetUserName(int userId);
        void AddUserFile(UserFile userFile, IFormFile file);
        int? DeleteUserFile(int id);
        public bool login(string username, string password);

        bool LoginApi(LoginDto user);

        bool regesterApi(regestirUserDto userDto);
    }
}