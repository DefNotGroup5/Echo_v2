using Domain.Account.DTOs;

namespace HTTPClients.ClientInterfaces;

public interface IItemService
{
    public Task<string?> CreateAsync(ItemCreationDto itemCreationDto);
    public Task<string?> GetById(int id);
    public Task<ICollection<string?>> GetAsync();
}