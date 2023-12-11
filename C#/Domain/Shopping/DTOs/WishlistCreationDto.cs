using Domain.Account.Models;
using Domain.Shopping.Models;

namespace Domain.Shopping.DTOs;

public class WishlistCreationDto
{
    public Item ItemId { get; set; }
    public User UserId { get; set; }

    public WishlistCreationDto(Item itemId, User userId)
    {
        ItemId = itemId;
        UserId = userId;
    }
}