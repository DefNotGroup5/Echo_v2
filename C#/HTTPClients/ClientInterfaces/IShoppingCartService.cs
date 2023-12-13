using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IShoppingCartService
{
    public Task<CartItem?> CreateAsync(CartItemCreationDto dto);
    public Task<ICollection<CartItem>?> GetByCustomerId(int id);
}