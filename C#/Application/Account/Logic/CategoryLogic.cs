using Application.Account.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using GrpcClientServices;
using CategoryService = GrpcClientServices.Services.CategoryService;

namespace Application.Account.Logic;

public class CategoryLogic : ICategoryLogic
{
    private readonly CategoryService _categoryService;
    
    public async Task<Category?> AddCategory(CategoryCreationDto dto)
    {
       /* try
        {
            Category? category = await _categoryService.GetCategoryByNameAsync(dto.CategoryName);
            if (category != null)
                throw new Exception("Category already exists");
            string validated = await ValidateCategory(dto);
            if (!string.IsNullOrEmpty(validated))
                throw new Exception(validated);
            Category categoryToCreate = new Category(dto.CategoryName)
            {
                CategoryName = dto.CategoryName,
            };
            
            Category? categoryCreated = await _categoryService.AddCategoryAsync(categoryToCreate);
            return categoryCreated;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }*/

       return null;
    }

    


  /*  private async Task<string> ValidateCategory(CategoryCreationDto dto)
    {
        string validated = "";
        Category? category = await _categoryService(dto.CategoryName);
        if (category != null)
        {
            validated = "A category with this name already exists";
            return validated;
        }
        if (string.IsNullOrEmpty(dto.CategoryName))
        {
            validated = "Please name this category!";
            return validated;
        }

        return validated;
    }*/
    
}