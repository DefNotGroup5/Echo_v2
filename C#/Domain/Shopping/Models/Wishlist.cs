using Domain.Account.Models;

namespace Domain.Shopping.Models;

public class Wishlist
{
    public int Id { get; set; }
    public Item ItemId { get; set; }
    public User UserId { get; set; }

    public Wishlist(Item itemId, User userId)
    {
        ItemId = itemId;
        UserId = userId;
    }

}