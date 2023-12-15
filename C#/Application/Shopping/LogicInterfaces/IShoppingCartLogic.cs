using System.Collections;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IShoppingCartLogic
{
    Task<CartItem?> AddAsync(CartItemCreationDto dto);
    Task<ICollection<CartItem>?> GetAllAsync();
    Task<ICollection<CartItem>?> GetAllByCustomerId(int id);
    Task ClearCart(int customerId);
}