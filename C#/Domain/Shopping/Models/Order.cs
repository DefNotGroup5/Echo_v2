using System.ComponentModel.DataAnnotations;

namespace Domain.Shopping.Models;

public class Order
{
    public ICollection<int> ItemIds { get; set; }
    public double TotalPrice { get; set; }
    public string? OrderId { get; set; }
    public string? Status { get; set; }
    public DateTime OrderDate { get; set; }
    public int CustomerId { get; set; }
    
    public Order()
    {
        ItemIds = new List<int>();
    }
    
}