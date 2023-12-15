using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public interface ICategoryLogic
{
    Task<Category?> AddCategory(CategoryCreationDto dto);

    Task<Category?> GetCategoryByName(string name);

    Task<ICollection<Category?>> GetAllCategories();

    Task<Category?> DeleteCategory(string name);

}