namespace Domain.Shopping.DTOs;

public class CartItemCreationDto
{
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public int ItemId { get; set; }
}