namespace Domain.Shopping.Models;

public class CartItem
{
    public int CustomerId { get; set; }
    public int Id { get; set; }
    public int Quantity { get; set; }
    public int ItemId { get; set; }
}