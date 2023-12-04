namespace Domain.Account.DTOs;

public class ItemCreationDto
{
    public int Id { get; }
    public String Name { get; set; }
    public int Price { get; set; }
    public int Quantity { get; set; }
    public String Description { get; set; }
    public String ImageUrl { get; set; }
    public int Stock { get; set; }

    public ItemCreationDto(int id, string name, int price, int quantity, string description, string imageUrl, int stock)
    {
        Id = 0;
        Name = name;
        Price = price;
        Quantity = quantity;
        Description = description;
        ImageUrl = imageUrl;
        Stock = stock;
    }
}