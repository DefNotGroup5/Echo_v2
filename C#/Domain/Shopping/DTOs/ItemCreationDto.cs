namespace Domain.Account.DTOs;

public class ItemCreationDto
{
    public int SellerId { get; set; }
    public String Name { get; set; }
    public String ImageUrl { get; set; }
    public String Description { get; set; }
    public double Price { get; set; }
    public int Quantity{ get; set; }

    public ItemCreationDto(int sellerId, string name, string imageUrl, string description, double price, int quantity)
    {
        SellerId = sellerId;
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        Quantity = quantity;
    }
}