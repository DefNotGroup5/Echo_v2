namespace Domain.Account.Models;

public class Item
{
    public int Id { get; set; }
    
    public int SellerId { get; set;}
    public String Name { get; set; }
    public String ImageUrl { get; set; }
    public String Description { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }

    public Item(string name, string imageUrl, string description)
    {
        Name = name;
        ImageUrl = imageUrl;
        Description = description;
    }
}