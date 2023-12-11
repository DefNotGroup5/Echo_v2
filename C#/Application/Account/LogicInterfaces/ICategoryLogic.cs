using Domain.Account.DTOs;
using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public interface ICategoryLogic
{
    Task<Category?> AddCategory(CategoryCreationDto dto);
    
   
    
}