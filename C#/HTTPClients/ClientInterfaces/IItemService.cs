using Domain.Account.DTOs;
using Domain.Account.Models;

namespace HTTPClients.ClientInterfaces;

public interface IItemService
{
    public Task<Item?> CreateAsync(ItemCreationDto itemCreationDto);
    public Task<Item?> GetById(int id);
    public Task<ICollection<Item?>?> GetAsync();
}