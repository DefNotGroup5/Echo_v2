using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IItemLogic
{
    Task<Item?> CreateItem(ItemCreationDto dto);
    Task<Item?> GetItemById(int id);
    Task<ICollection<Item?>> GetItems();
}