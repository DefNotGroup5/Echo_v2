using System.Security.Claims;
using Application.Account.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Grpc.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Account_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController
{
    
    private readonly ICategoryLogic _categoryLogic;
    private readonly IConfiguration _config;
    
    
    public CategoryController(IConfiguration config, ICategoryLogic categoryLogic)
    {
        _config = config;
        _categoryLogic = categoryLogic;
    }
    

    [HttpPost("Add")]
    public async Task<ActionResult<Category>> AddCategory(CategoryCreationDto categoryCreationDto)
    {
        try
        {
            Category? category = await _categoryLogic.AddCategory(categoryCreationDto);
            if (category != null)
                return Created($"/Category/{category.Id}", category);
            throw new Exception("Error when adding a category!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    

    [HttpGet]
    public async Task<ActionResult<ICollection<CategoryCreationDto>>> GetAllCategoriesA()
    {
        try
        {
            ICollection<CategoryCreationDto> passingCategories = new List<CategoryCreationDto>();
            ICollection<Category?> categories = await _categoryLogic.GetAllCategories();
            foreach (var category in categories)
            {
                CategoryCreationDto dto;

                if (category is not null)
                {
                    dto = new CategoryCreationDto(category.CategoryName)
                    {
                        CategoryName = category.CategoryName
                    };


                    passingCategories.Add(dto);
                }
            }

            return Ok(passingCategories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<CategoryCreationDto>> GetCategoryByName([FromRoute] string name)
    {
        try
        {
            CategoryCreationDto dto = null;
            ICollection<Category?> categories = await _categoryLogic.GetAllCategories();
            foreach (var category in categories)
            {
                if (category is not null)
                {
                    dto = new CategoryCreationDto(category.CategoryName)
                    {
                        CategoryName = category.CategoryName
                    };
                }
               
            }
            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
    
