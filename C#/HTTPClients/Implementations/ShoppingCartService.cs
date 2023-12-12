using Domain.Shopping.Models;
using HTTPClients.ClientInterfaces;
using Microsoft.AspNetCore.Identity;

namespace HTTPClients.Implementations;

public class ShoppingCartService: IShoppingCartService
{
    public event Action<ShoppingCart?>? OnShoppingCartChanged;
    private ShoppingCart? _shoppingCart;

    public ShoppingCart GetShoppingCart() => _shoppingCart ??= new ShoppingCart();

    public void UpdateShoppingCart(ShoppingCart shoppingCart) => _shoppingCart = shoppingCart;

    public async Task AddItemToShoppingCart(Item item, int quantity)
    {
        if (string.IsNullOrEmpty(UserHttpClient.Jwt))
        {
            return;
        }
        _shoppingCart?.ItemsInCart.Add(item);
        Console.WriteLine(_shoppingCart.ItemsInCart.Count);
        NotifyShoppingCartChanged();
    }

    public async Task RemoveItemFromShoppingCart(int itemId)
    {
        if (string.IsNullOrEmpty(UserHttpClient.Jwt))
        {
            return;
        }

        if (_shoppingCart?.ItemsInCart != null)
        {
            foreach (var item in _shoppingCart.ItemsInCart)
            {
                if (item.Id == itemId)
                {
                    _shoppingCart.ItemsInCart.Remove(item);
                    NotifyShoppingCartChanged();
                }
            }
        }
    }
    
    public void NotifyShoppingCartChanged()
    {
        // Notify listeners about the shopping cart change
        OnShoppingCartChanged?.Invoke(_shoppingCart);
    }
}