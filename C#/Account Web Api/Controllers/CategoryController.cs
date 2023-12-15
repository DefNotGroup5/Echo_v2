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
public class CategoryController : ControllerBase
{
    
    private readonly ICategoryLogic _categoryLogic;


    public CategoryController(ICategoryLogic categoryLogic)
    {
        _categoryLogic = categoryLogic;
    }
    

    [HttpPost]
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
    public async Task<ActionResult<ICollection<Category>>> GetAllCategoriesAsync()
    {
        try
        {
            Console.WriteLine("reached!");
            return Ok(await _categoryLogic.GetAllCategories());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<Category>> GetCategoryByName([FromRoute] string name)
    {
        try
        {
            return Ok(await _categoryLogic.GetCategoryByName(name));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{name}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] string name)
    {
        try
        {
            await _categoryLogic.DeleteCategory(name);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}
    
