using Microsoft.AspNetCore.Http;
using resturant.Application.Dtos;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;
using resturant.Application.Services.Base;

namespace resturant.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private string UploadImage(IFormFile image, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(image.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "Users"
            );

            // Create folder if it doesn't exist
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(
                folderPath,
                fileName
            );

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return "/images/Users/" + fileName;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _unitOfWork.UserRepo.GetAll();

            var usersDto = users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Username = u.Username,
                ImageURL = u.ImageURL
            }).ToList();

            return usersDto;
        }

        public void CreateUser(User user)
        {
            if (user.image != null)
            {
                user.ImageURL = UploadImage(user.image, user.Name);
            }

            // Hash the password before saving it to the database
            user.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _unitOfWork.UserRepo.Add(user);
            _unitOfWork.Save();
        }

        public UpdateUserDto? GetForEdit(int id)
        {
            var user = _unitOfWork.UserRepo.GetById(id);
            if (user == null) return null;

            return new UpdateUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Password = user.Password
            };
        }

        public void UpdateUser(UpdateUserDto userDto)
        {
            var user = _unitOfWork.UserRepo.GetById(userDto.Id);
            if (user == null) return;

            user.Email = userDto.Email;
            user.Name = userDto.Name;
            user.Username = userDto.Username;

            if (!string.IsNullOrEmpty(userDto.Password))
            {
                user.Password = userDto.Password;
                user.HashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            }

            if (userDto.image != null)
            {
                user.ImageURL = UploadImage(userDto.image, userDto.Name);
            }

            _unitOfWork.UserRepo.Update(user);
            _unitOfWork.Save();
        }

        public void DeleteUser(int id)
        {
            var user = _unitOfWork.UserRepo.GetById(id);
            if (user != null)
            {
                _unitOfWork.UserRepo.Delete(user);
                _unitOfWork.Save();
            }
        }

        public UserRolesVM? GetUserRoles(int id)
        {
            var user = _unitOfWork.UserRepo.GetById(id);
            if (user == null) return null;

            var roles = _unitOfWork.RoleRepo.GetAll();
            var userRoleIds = _unitOfWork.RoleUserRepo.GetSelectedRoles(id);

            return new UserRolesVM
            {
                UserId = user.Id,
                UserName = user.Name,
                Roles = roles.Select(role => new RoleCheckVM
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    IsSelected = userRoleIds.Contains(role.Id)
                }).ToList()
            };
        }

        public void UpdateUserRoles(UserRolesVM model)
        {
            var user = _unitOfWork.UserRepo.GetById(model.UserId);
            if (user == null) return;

            var oldRoles = _unitOfWork.RoleUserRepo.GetRolesByUserId(model.UserId);
            _unitOfWork.RoleUserRepo.RemoveRange(oldRoles);

            foreach (var role in model.Roles)
            {
                if (role.IsSelected)
                {
                    var roleUser = new RoleUser
                    {
                        UserId = model.UserId,
                        RoleId = role.RoleId
                    };
                    _unitOfWork.RoleUserRepo.Add(roleUser);
                }
            }

            _unitOfWork.Save();
        }

        public IEnumerable<UserFile> GetUserFiles(int userId)
        {
            return _unitOfWork.UserFileRepo.GetFilesByUserId(userId);
        }

        public string? GetUserName(int userId)
        {
            return _unitOfWork.UserRepo.GetById(userId)?.Name;
        }

        public void AddUserFile(UserFile userFile, IFormFile file)
        {
            userFile.FileURL = UploadImage(file, userFile.Name);
            _unitOfWork.UserFileRepo.Add(userFile);
            _unitOfWork.Save();
        }

        public int? DeleteUserFile(int id)
        {
            var userfile = _unitOfWork.UserFileRepo.GetById(id);
            if (userfile == null) return null;

            int userId = userfile.UserId;
            _unitOfWork.UserFileRepo.Delete(userfile);
            _unitOfWork.Save();

            return userId;
        }

        public bool login(string username, string password) {
            var user = _unitOfWork.UserRepo.GetUserByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.HashPassword)) { 
                return true;
            }
            return false;
        }

        public bool LoginApi(LoginDto userDto) {
            var user = _unitOfWork.UserRepo.GetUserByUsername(userDto.Username);


            if (user == null ||
                string.IsNullOrEmpty(user.HashPassword))
            {
                return false;
            }

            bool userFound = BCrypt.Net.BCrypt.Verify(
                userDto.Password,
                user.HashPassword
            );

            return userFound;

        }

        public bool regesterApi(regestirUserDto userDto) {
            var checkuser = _unitOfWork.UserRepo.GetUserByUsername(userDto.Username);
            var checkemail = _unitOfWork.UserRepo.GetUserByEmail(userDto.Email);

            if (checkuser != null || checkemail != null) { 
                return false;
            }

            var user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Username = userDto.Username,
                Password = userDto.Password,
                HashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password)
              
            };

            _unitOfWork.UserRepo.Add(user);

            _unitOfWork.Save();

            return true;



        }
    }
}