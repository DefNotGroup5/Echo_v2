namespace Domain.Account.DTOs;

public class ItemCreationDto
{
    public int SellerId { get; set; }
    public String Name { get; set; }
    public String ImageUrl { get; set; }
    public String Description { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }

    public ItemCreationDto(int sellerId, string name, string imageUrl, string description, int price, int stock)
    {
        sellerId = 0;
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        Stock = stock;
    }
}