using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Application.Dtos;
using resturant.Application.Services.Base;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;

namespace resturant.Application.Services
{
    public class MenuItemsServices : IMenuItemsServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuItemsServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        //method to have a url for image and save in db
        private string UploadImage(IFormFile image, string name)
        {
            string fileName = name + "_" + Guid.NewGuid().ToString()
                              + Path.GetExtension(image.FileName);

            string folderPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "images",
                "Items"
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

            return "/images/Items/" + fileName;
        }

        public void CreateMenuItemDto(CreateMenuItemDto itemDto)
        {
            var item = new MenuItem();

            if (itemDto.image != null) {
                item.ImageURL = UploadImage(itemDto.image,itemDto.name);
            }

            item.name = itemDto.name;
            item.price = itemDto.price;
            item.description = itemDto.description;
            item.enabled = itemDto.enabled;
            item.categoryid = itemDto.categoryid;

            _unitOfWork.MenuItemRepo.Add(item);
            _unitOfWork.Save();
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categ = _unitOfWork.CategoryRepo.GetAll();


            var categDto = categ.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();


            return categDto;
        }

        public IEnumerable<MenuItemDto> GetAllWithCategory()
        {
            var items =  _unitOfWork.MenuItemRepo.GetAllWithCategory();
            var itemsDto = items.Select(x => new MenuItemDto
            {
                id = x.id,
                name = x.name,
                price = x.price,
                description = x.description,
                enabled = x.enabled,
                CategoryName = x.Category?.Name,
                ImageURL = x.ImageURL
            }).ToList();

            return itemsDto;
        }
        //---------------------------------------
        // جلب قائمة التصنيفات لعرضها في الـ Dropdown List
        public SelectList GetCategoriesSelectList()
        {
            var categories = _unitOfWork.CategoryRepo.GetAll();
            return new SelectList(categories, "Id", "Name");
        }

        // جلب بيانات العنصر للتعديل
        public UpdateMenuItemDto GetForEdit(int id)
        {
            var item = _unitOfWork.MenuItemRepo.GetById(id);
            if (item == null) return null;

            return new UpdateMenuItemDto
            {
                id = item.id,
                name = item.name,
                price = item.price,
                description = item.description,
                enabled = item.enabled,
                categoryid = item.categoryid,
                ImageURL = item.ImageURL
            };
        }

        // حفظ التعديل
        public void UpdateMenuItem(UpdateMenuItemDto itemDto)
        {
            var item = _unitOfWork.MenuItemRepo.GetById(itemDto.id);
            if (item == null) return;

            item.name = itemDto.name;
            item.price = itemDto.price;
            item.description = itemDto.description;
            item.enabled = itemDto.enabled;
            item.categoryid = itemDto.categoryid;

            // فحص رفع صورة جديدة
            if (itemDto.image != null)
            {
                item.ImageURL = UploadImage(itemDto.image, itemDto.name);
            }

            _unitOfWork.MenuItemRepo.Update(item);
            _unitOfWork.Save();
        }

        // جلب بيانات العنصر لتأكيد الحذف
        public MenuItemDto GetForDelete(int id)
        {
            var item = _unitOfWork.MenuItemRepo.GetById(id);
            if (item == null) return null;

            return new MenuItemDto
            {
                id = item.id,
                name = item.name,
                price = item.price,
                description = item.description
            };
        }

        // تنفيذ الحذف عبر الـ Id
        public void DeleteMenuItem(int id)
        {
            var item = _unitOfWork.MenuItemRepo.GetById(id);
            if (item != null)
            {
                _unitOfWork.MenuItemRepo.Delete(item);
                _unitOfWork.Save();
            }
        }

        public MenuItemDto? GetMenuItemById(int id)
        {
            var item = _unitOfWork.MenuItemRepo.GetById(id);
            if (item == null) return null;

            return new MenuItemDto
            {
                id = item.id,
                name = item.name,
                price = item.price,
                description = item.description,
                enabled = item.enabled,
                ImageURL = item.ImageURL
            };
        }
    }
}
