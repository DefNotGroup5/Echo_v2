using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class WishlistLogic : IWishlistLogic
{
    
    private readonly WishlistService _wishlistService;
    private readonly ItemService _itemService;
    private readonly UsersService _usersService;

    public WishlistLogic(WishlistService wishlistService, ItemService itemService, UsersService usersService)
    {
        _wishlistService = wishlistService;
        _itemService = itemService;
        _usersService = usersService;
    }

    public async Task<Wishlist?> CreateWishlistItemAsync(WishlistCreationDto dto)
    {
        try
        {
            string validation = await ValidateWishlistCreationDto(dto);
            if (!string.IsNullOrEmpty(validation))
            {
                throw new Exception(validation);
            }
            Wishlist? wishlist = await _wishlistService.AddWishlistItemAsync(dto);
            return wishlist;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }

    public async Task<ICollection<Wishlist?>> GetWishlistByUserAsync(int id)
    {
        try
        {
            ICollection<Wishlist?> wishlist = await _wishlistService.GetWishlistByUserIdAsync(id);
            return wishlist;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task RemoveWishlist(int id)
    {
        try
        {
            await _wishlistService.RemoveWishlist(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string> ValidateWishlistCreationDto(WishlistCreationDto dto)
    {
        
        User? user = await _usersService.GetByIdAsync(dto.UserId);
        if (user == null)
        {
            return "User does not exist!";
        }

        Item? item = await _itemService.GetItemByIdAsync(dto.ItemId);
        if (item == null)
        {
            return "Item does not exist!";
        }

        foreach (var wishlist in await _wishlistService.GetWishlistByUserIdAsync(dto.UserId))
        {
            if (wishlist.ItemId == dto.ItemId)
            {
                return "Item is already in wishlist!";
            }
        }
        return "";
    }
}