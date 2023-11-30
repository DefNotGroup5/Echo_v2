using Application.Shopping.LogicInterfaces;
using Domain.Account.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class ItemLogic : IItemLogic
{

    private readonly string _itemsService;
    private readonly UsersService _usersService;

    public ItemLogic(string itemsService, UsersService usersService)
    {
        _usersService = usersService;
        _itemsService = itemsService;
    }

    public async Task<string?> CreateItem(string itemCreationDto)
    {
        try
        {
            ValidateCreationDto(itemCreationDto);
            /*
            Item itemToCreate = new Item()
            Item item = await _itemsService.CreateAsync(itemToCreate);
            */
            return string.Empty;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<string?> GetItemById(int id)
    {
        /*
        Item item = await _itemsService.GetByIdAsync(id); 
         */
        return null;
    }

    public async Task<ICollection<string?>> GetItems()
    {
        /*
        ICollection<Item> items = await _itemsService.GetItems();
         */
        return new List<string?>();
    }

    private async void ValidateCreationDto(string itemCreationDto)
    {
        User? user = await _usersService.GetByIdAsync(23);
        if (user == null)
            throw new Exception("Error: User with such id does not exist!");
        /*
        if(itemCreationDto.Price < 0)
            throw new Exception("Price is an invalid number!");
        if(itemCreationDto.Name == null || itemCreationDto.Name.Equals(""))
            throw new Exception("Name cannot be empty!");
        if(itemCreationDto.Description == null || itemCreationDto.Description.Equals(""))
            throw new Exception("Description cannot be empty!");    
        if(itemCreationDto.Description == null || itemCreationDto.Description.Equals(""))
            throw new Exception("Description cannot be empty!");
        */
    }
}