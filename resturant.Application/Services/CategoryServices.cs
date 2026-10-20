using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Application.Dtos;
using resturant.Domain.Models;
using resturant.Infrastructure.Repositories.Base;
using resturant.Application.Services.Base;

namespace resturant.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            var categ= _unitOfWork.CategoryRepo.GetAll();

            var categDto = categ.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();

            return categDto;
        }

        public SelectList GetCategoriesSelectList()
        {
            var categories = _unitOfWork.CategoryRepo.GetAll();
            return new SelectList(categories, "Id", "Name");
        }

        public void CreateCategory(CreateCategoryDto dto)
        {
            var category = new Category
            {
                Name = dto.Name
            };

            _unitOfWork.CategoryRepo.Add(category);
            _unitOfWork.Save();
        }

        public UpdateCategoryDto GetForEdit(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            if (category == null) return null;

            return new UpdateCategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public void UpdateCategory(UpdateCategoryDto dto)
        {
            var category = new Category
            {
                Id = dto.Id,
                Name = dto.Name
            };

            _unitOfWork.CategoryRepo.Update(category);
            _unitOfWork.Save();
        }

        public CategoryDto GetForDelete(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            if (category == null) return null;

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }

        public void DeleteCategory(int id)
        {
            var category = _unitOfWork.CategoryRepo.GetById(id);
            if (category != null)
            {
                _unitOfWork.CategoryRepo.Delete(category);
                _unitOfWork.Save();
            }
        }
    }
}
