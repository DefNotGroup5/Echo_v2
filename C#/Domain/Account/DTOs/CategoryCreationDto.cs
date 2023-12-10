namespace Domain.Account.DTOs;

public class CategoryCreationDto
{
    public long Id { get; set; }
    
    public string CategoryName { get; set; }


    public CategoryCreationDto(long id, string categoryName)
    {
        Id = id;
        CategoryName = categoryName;
    }
    
    
    
}