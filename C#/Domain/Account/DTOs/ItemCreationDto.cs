namespace Domain.Account.DTOs;

public class ItemCreationDto
{
    public int Id { get; }
    public String Name { get; set; }
    public String ImageUrl { get; set; }
    public String Description { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }

    public ItemCreationDto(int id, string name, string imageUrl, string description, int price, int stock)
    {
        Id = 0;
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
        Price = price;
        Stock = stock;
    }
}