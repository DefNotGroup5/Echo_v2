using Domain.Shopping.Models;

namespace Domain.Shopping.DTOs;

public class OrderCreationDto
{
    public ICollection<int> ItemIds { get; set; }
    public int CustomerId { get; set; }
}