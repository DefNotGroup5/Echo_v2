namespace Domain.Account.Models;

public class Category
{
    public string CategoryName { get; set; }
    
    public int Id { get; set; }

    public Category(string categoryName)
    {
        CategoryName = categoryName;
    }
}