using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IWishlistService
{
    public Task<Wishlist?> CreateAsync(WishlistCreationDto wishlistCreationDto);
    public Task<ICollection<Wishlist?>?> GetByUserIdAsync(int id);
    public Task RemoveWishlist(int id);
}