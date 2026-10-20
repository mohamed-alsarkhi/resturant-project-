using Microsoft.AspNetCore.Mvc.Rendering;
using resturant.Application.Dtos;
using resturant.Domain.Models;

namespace resturant.Application.Services.Base
{
    public interface IMenuItemsServices
    {
        IEnumerable<MenuItemDto> GetAllWithCategory();
        public void CreateMenuItemDto(CreateMenuItemDto menuItemDto);
        IEnumerable<CategoryDto> GetAllCategories();
        //-----------------------------------
        SelectList GetCategoriesSelectList();
        UpdateMenuItemDto GetForEdit(int id);
        void UpdateMenuItem(UpdateMenuItemDto itemDto);
        MenuItemDto GetForDelete(int id);
        void DeleteMenuItem(int id);
        MenuItemDto? GetMenuItemById(int id);
    }
}
