namespace Domain.Account.Models;

public class ShoppingCart
{
    public ICollection<Item> ItemsInCart { get; set; }
    public ShoppingCart()
    {
        ItemsInCart = new List<Item>();
    }
}