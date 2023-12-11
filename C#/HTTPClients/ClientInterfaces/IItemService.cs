using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IItemService
{
    public Task<Item?> CreateAsync(ItemCreationDto itemCreationDto);
    public Task<Item?> GetById(int id);
    public Task<ICollection<Item?>?> GetAsync();
}