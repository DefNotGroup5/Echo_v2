using Domain.Shopping.Models;

namespace Domain.Shopping.DTOs;

public class OrderCreationDto
{
    public ICollection<Item> ItemsInOrder { get; set; }
    public double TotalPrice { get; set; }
    public int CustomerId { get; set; }
}