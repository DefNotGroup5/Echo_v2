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
        try
        {
            string validation = await ValidateCategory(dto);
            if (string.IsNullOrEmpty(validation))
            {
                throw new Exception(validation);
            }
            
            Category categoryAdded = new Category(dto.CategoryName)
                {
                    CategoryName = dto.CategoryName
                };
                Category? category = await _categoryService.AddCategoryAsync(categoryAdded);
                return category;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Category?> GetCategoryByName(CategoryCreationDto dto)
    {
        try
        {
            string validation = await ValidateCategory(dto);
            if (string.IsNullOrEmpty(validation))
            {
                throw new Exception(validation);
            }

            Category? category = await _categoryService.GetCategoryByNameAsync(dto.CategoryName);
            return category;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<ICollection<Category>?> GetAllCategories()
    {
        try
        {
            ICollection<Category?> categories = await _categoryService.GetAllCategories();
            return categories;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Category?> DeleteCategory(CategoryCreationDto dto)
    {
        try
        {
            Category? category = await _categoryService.DeleteCategory(dto.CategoryName);
            return category;
            Console.WriteLine("Category successfully deleted!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return null;
    }

    



    private async Task<string> ValidateCategory(CategoryCreationDto dto)
    {
        string validated = "";
        Category? category = await _categoryService.GetCategoryByNameAsync(dto.CategoryName);
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
    }
    
}