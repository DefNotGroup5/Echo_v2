using Domain.Account.Models;

namespace Domain.Account.DTOs;

public class CategoryCreationDto
{
    public int Id { get; set; }
    
    public string? CategoryName { get; set; }


    public CategoryCreationDto(string categoryName)
    {
        Id = 0;
        CategoryName = categoryName;
    }
    
}