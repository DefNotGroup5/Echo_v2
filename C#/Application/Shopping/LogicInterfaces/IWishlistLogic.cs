using Domain.Account.Models;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IWishlistLogic
{
    Task<Wishlist?> CreateWishlistItemAsync(WishlistCreationDto dto);
    Task<ICollection<Wishlist?>> GetWishlistByUserAsync(int id);
}