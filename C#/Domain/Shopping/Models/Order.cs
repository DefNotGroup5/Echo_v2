namespace Domain.Shopping.Models;

public class Order
{
    public ICollection<Item> ItemsInOrder { get; set; }
    public double TotalPrice { get; set; }
    public int Id { get; set; }
    public int CustomerId { get; set; }
    
    public Order(int customerId)
    {
        ItemsInOrder = new List<Item>();
        TotalPrice = 0;
        CustomerId = customerId;
    }
}