using Microsoft.AspNetCore.Mvc;
using resturant.Data;
using resturant.Dtos;
using resturant.Models;
using resturant.Repositories;

namespace resturant.Controllers
{
    
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository; 

         private readonly IRoleRepository _roleRepository; 

         private readonly IRoleUserRepository _roleUserRepository; 

         private readonly IUserFileRepository _userFileRepository;

        public UsersController(IUserRepository userRepository , IRoleRepository roleRepository , IRoleUserRepository roleUserRepository, IUserFileRepository userFileRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _roleUserRepository = roleUserRepository;
            _userFileRepository = userFileRepository;
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





        public IActionResult Index()
        {
            var users = _userRepository.GetallImprove();
            return View(users);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user )
        {
            if (ModelState.IsValid)
            {
                if (user.image != null) {
                    user.ImageURL = UploadImage(user.image, user.Name);
                }

                // Hash the password before saving it to the database
                user.HashPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _userRepository.Add(user);
                _userRepository.Save();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        public IActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UpdateUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Password= user.Password
            };
            return View(userDto);
        }

        [HttpPost]
        public IActionResult Edit(UpdateUserDto userDto)
        {

            if (ModelState.IsValid)
            {
                var user = _userRepository.GetById(userDto.Id);

                if (user == null)
                    return NotFound();

                user.Email = userDto.Email;
                user.Name = userDto.Name;
                user.Username = userDto.Username;



                if (!string.IsNullOrEmpty(userDto.Password))
                {
                    user.Password = userDto.Password;
                    user.HashPassword =
                        BCrypt.Net.BCrypt.HashPassword(userDto.Password);
                }


                if (userDto.image != null)
                {
                    user.ImageURL = UploadImage(userDto.image, userDto.Name);
                }
                //else {
                //    user.ImageURL = olduser.ImageURL;
                //}
                
                _userRepository.Update(user);
                _userRepository.Save();
                return RedirectToAction("Index");
            }
            return View(userDto);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var user = _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            _userRepository.Delete(user);
            _userRepository.Save();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ManageRoles(int id)
        {
            var user = _userRepository.GetById(id);

            if (user == null)
            {
                return NotFound();
            }

            // جميع الصلاحيات
            var roles = _roleRepository.GetallDto();

            // الصلاحيات الموجودة بالفعل للمستخدم
            var userRoleIds = _roleUserRepository.GetSelectedRoles(id);

            var model = new UserRolesVM
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

            return View(model);
        }


        [HttpPost]
        public IActionResult ManageRoles(UserRolesVM model)
        {
            var user = _userRepository.GetById(model.UserId);

            if (user == null)
            {
                return NotFound();
            }

            // Get old roles
            var oldRoles = _roleUserRepository.GetRolesByUserId(model.UserId);

            // Remove old roles
            _roleUserRepository.RemoveRange(oldRoles);

            // Add selected roles
            foreach (var role in model.Roles)
            {
                if (role.IsSelected)
                {
                    RoleUser roleUser = new RoleUser
                    {
                        UserId = model.UserId,
                        RoleId = role.RoleId
                    };

                    _roleUserRepository.Add(roleUser);
                }
            }

            _roleUserRepository.Save();

            return RedirectToAction("Index");
        }


        public IActionResult ManageFiles(int UserId)
        {
            var user = _userRepository.GetById(UserId);
            if (user == null)
            {
                return NotFound();
            }

            var files = _userFileRepository.GetFilesByUserId(UserId);

            ViewBag.Files = files;

            var userFile = new UserFile();

            userFile.UserId = UserId;

            ViewBag.userName = user.Name;


            return View(userFile);
        }

        [HttpPost]
        public IActionResult ManageFiles(UserFile userFile, IFormFile file)
        {
            if (file == null)
            {
                ViewBag.Files = _userFileRepository.GetFilesByUserId(userFile.UserId);
                var user = _userRepository.GetById(userFile.UserId);
                ViewBag.userName = user.Name;
                return View(userFile);
            }

            userFile.FileURL = UploadImage(file, userFile.Name);


            if (!ModelState.IsValid)
            {
                return View(userFile);
            }

            _userFileRepository.Add(userFile);
            _userFileRepository.Save();

            return RedirectToAction("ManageFiles", new { UserId = userFile.UserId });

        }

        [HttpPost]
        public IActionResult DeleteFile(int id) {
            var userfile = _userFileRepository.GetById(id);
            if (userfile == null) {
                return NotFound();
            }
            var userid= userfile.UserId;

            _userFileRepository.Delete(userfile);
            _userFileRepository.Save();


            return RedirectToAction("ManageFiles", new { UserId = userid });
        }
    }
}
