using Application.Shopping.LogicInterfaces;
using Domain.Account.DTOs;
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

    public async Task<Item?> CreateItem(ItemCreationDto dto)
    {
        //Item needs ID
        try
        {
           ValidateCreationDto(dto);
           Item itemToCreate = new Item()
           {
               Description = dto.Description,
               Name = dto.Name,
               Price = dto.Price,
               Stock = dto.Stock,
               ImageUrl = dto.ImageUrl
           };
           //Item item = await _itemsService.CreateAsync(itemToCreate);
            
            return new Item();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<Item?> GetItemById(int id)
    {
        /*
        Item item = await _itemsService.GetByIdAsync(id); Need this method
         */
        return null;
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
        //I need Seller ID
        User? user = await _usersService.GetByIdAsync(23);
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