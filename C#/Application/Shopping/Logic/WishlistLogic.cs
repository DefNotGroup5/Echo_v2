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
            Wishlist wishlistItemToCreate = new Wishlist(dto.ItemId, dto.UserId)
            {
                ItemId = dto.ItemId,
                UserId = dto.UserId
            };
            Wishlist? wishlist = await _wishlistService.AddWishlistItemAsync(wishlistItemToCreate);
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
        ICollection<Wishlist?> wishlist = await _wishlistService.GetWishlistByUserIdAsync(id);
        return wishlist;
    }
}