using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public interface ICategoryLogic
{
    Task<Category?> AddCategory(CategoryCreationDto dto);

    Task<Category?> GetCategoryByName(CategoryCreationDto dto);

    Task<ICollection<Category>?> GetAllCategories();

    Task<Category?> DeleteCategory(CategoryCreationDto dto);

}