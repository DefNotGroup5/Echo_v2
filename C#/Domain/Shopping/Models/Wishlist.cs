using Domain.Shopping.Models;

namespace Domain.Shopping.Models;

public class Wishlist
{
    public int Id { get; set; }
    public int ItemId { get; set; }
    public int UserId { get; set; }

}