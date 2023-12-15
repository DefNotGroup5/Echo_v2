using Domain.Account.DTOs;
using Domain.Account.Models;

namespace HTTPClients.ClientInterfaces;

public interface ICategoryService
{
    public Task<Category?> AddCategoryAsync(CategoryCreationDto categoryCreationDto);
    public Task<Category?> GetCategoryByName(string categoryName);
    public Task<ICollection<Category?>> GetAllCategories();

    public Task DeleteCategory(string categoryName);
    
    


}