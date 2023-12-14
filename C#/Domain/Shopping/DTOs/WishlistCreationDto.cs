using Domain.Shopping.Models;

namespace Domain.Shopping.DTOs;

public class WishlistCreationDto
{
    public int ItemId { get; set; }
    public int UserId { get; set; }
    
}