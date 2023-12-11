using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IItemLogic
{
    Task<Item?> CreateItemAsync(ItemCreationDto dto);
    Task<Item?> GetItemByIdAsync(int id);
    Task<ICollection<Item?>> GetItemsAsync();
}