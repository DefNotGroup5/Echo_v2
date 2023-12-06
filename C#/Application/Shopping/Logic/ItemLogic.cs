using Application.Shopping.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class ItemLogic : IItemLogic
{

    private readonly ItemService _itemsService;
    private readonly UsersService _usersService;

    public ItemLogic(ItemService itemsService, UsersService usersService)
    {
        _usersService = usersService;
        _itemsService = itemsService;
    }

    public async Task<Item?> CreateItem(ItemCreationDto dto)
    {
        try
        {
           ValidateCreationDto(dto);
           Item itemToCreate = new Item()
           {
               Description = dto.Description,
               Name = dto.Name,
               Price = dto.Price,
               Stock = dto.Stock,
               ImageUrl = dto.ImageUrl,
               SellerId = dto.SellerId
           };
           int itemId = await _itemsService.AddItemAsync(itemToCreate);
           return await GetItemById(itemId, dto.SellerId);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public async Task<Item?> GetItemById(int id, int sellerId)
    {
        Item? item = await _itemsService.GetItemById(id, sellerId);
        return item;
    }

    public async Task<ICollection<Item?>> GetItems()
    {
        /*
        ICollection<Item> items = await _itemsService.GetItems(); Need this method
         */
        return new List<Item?>();
    }

    private async void ValidateCreationDto(ItemCreationDto itemCreationDto)
    {
        User? user = await _usersService.GetByIdAsync(itemCreationDto.SellerId);
        if (user == null)
            throw new Exception("Error: User with such id does not exist!");
        if(itemCreationDto.Price < 0)
            throw new Exception("Price is an invalid number!");
        if(itemCreationDto.Name == null || itemCreationDto.Name.Equals(""))
            throw new Exception("Name cannot be empty!");
        if(itemCreationDto.Description == null || itemCreationDto.Description.Equals(""))
            throw new Exception("Description cannot be empty!");    
        if(itemCreationDto.Stock < 0)
            throw new Exception("Stock must be either 0 or a positive number!");
        if (string.IsNullOrEmpty(itemCreationDto.ImageUrl))
            throw new Exception("Image must be chosen!");
    }
}