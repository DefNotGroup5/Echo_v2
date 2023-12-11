using Domain.Account.Models;

namespace Domain.Account.DTOs;

public class OrderCreationDto
{
    public ICollection<Item> ItemsInOrder { get; set; }
    public double TotalPrice { get; set; }
    public int CustomerId { get; set; }
}