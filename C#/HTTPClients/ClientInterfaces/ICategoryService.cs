using Domain.Account.DTOs;

namespace HTTPClients.ClientInterfaces;

public interface ICategoryService
{
    public Task<string?> AddCategoryAsync(CategoryCreationDto categoryCreationDto);
    public Task<string?> GetCategoryByName(string categoryName);
    public Task<ICollection<string?>> GetAllCategories();
}