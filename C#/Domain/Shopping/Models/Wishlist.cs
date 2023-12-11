using Domain.Shopping.Models;

namespace Domain.Shopping.Models;

public class Wishlist
{
    public int Id { get; set; }
    public Item ItemId { get; set; }
    public User UserId { get; set; }

    public Wishlist(int itemId, int userId)
    {
        ItemId.Id = itemId;
        UserId.Id = userId;
    }

}