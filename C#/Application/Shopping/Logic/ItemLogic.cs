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

    public async Task<Item?> CreateItemAsync(ItemCreationDto dto)
    {
        try
        {
           string validation = await ValidateCreationDto(dto);
           if (string.IsNullOrEmpty(validation))
           {
               throw new Exception(validation);
           }
           Item itemToCreate = new Item(dto.Name, dto.ImageUrl, dto.Description)
           {
               Price = dto.Price,
               Quantity = dto.Quantity,
               SellerId = dto.SellerId
           };
           Item? item = await _itemsService.AddItemAsync(itemToCreate);
           return item;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
    public async Task<Item?> GetItemByIdAsync(int id)
    {
        Item? item = await _itemsService.GetItemByIdAsync(id);
        return item;
    }

    public async Task<ICollection<Item?>> GetItemsAsync()
    {
        
        ICollection<Item?> items = await _itemsService.GetAllItemsAsync(); 
        return items;
    }

    private async Task<string> ValidateCreationDto(ItemCreationDto itemCreationDto)
    {
        string validation = "";
        User? user = await _usersService.GetByIdAsync(itemCreationDto.SellerId);
        if (user == null)
        {
            validation = "Error: User with such id does not exist!";
            return validation;
        }
        if(itemCreationDto.Price < 0)
        {
            validation = "Price is an invalid number!";
            return validation;
        }
        if(string.IsNullOrEmpty(itemCreationDto.Name))
        {
            validation = "Name cannot be empty!";
            return validation;
        }
        if(string.IsNullOrEmpty(itemCreationDto.Description))
        {
            validation = "Description cannot be empty!";
            return validation;
        }
        if(itemCreationDto.Quantity < 0)
        {
            validation = "Stock must be either 0 or a positive number!";
            return validation;
        }
        if (string.IsNullOrEmpty(itemCreationDto.ImageUrl))
        {
            validation = "Image must be chosen!";
            return validation;
        }
        return validation;
    }
}