using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Application.Dtos;

namespace resturant.Application.Services.Base
{
    public interface ICategoryServices
    {
        IEnumerable<CategoryDto> GetAllCategories();
        SelectList GetCategoriesSelectList();
        void CreateCategory(CreateCategoryDto dto);
        UpdateCategoryDto GetForEdit(int id);
        void UpdateCategory(UpdateCategoryDto dto);
        CategoryDto GetForDelete(int id);
        void DeleteCategory(int id);
    }
}
