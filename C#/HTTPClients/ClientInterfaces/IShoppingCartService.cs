using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IShoppingCartService
{
    public ShoppingCart GetShoppingCart();
    public void UpdateShoppingCart(ShoppingCart shoppingCart);
    Task AddItemToShoppingCart(Item item, int quantity);
    Task RemoveItemFromShoppingCart(int itemId);
    public event Action<ShoppingCart?>? OnShoppingCartChanged;
}