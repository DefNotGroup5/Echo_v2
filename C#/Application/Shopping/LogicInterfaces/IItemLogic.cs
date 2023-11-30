namespace Application.Shopping.LogicInterfaces;

public interface IItemLogic
{
    Task<string?> CreateItem(string itemCreationDto);
    Task<string?> GetItemById(int id);
    Task<string?> GetItems();
}