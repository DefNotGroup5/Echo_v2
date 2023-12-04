using Application.Shopping.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemLogic _itemLogic;

    public ItemsController(IItemLogic itemLogic)
    {
        _itemLogic = itemLogic;
    }

    [HttpPost]
    public async Task<ActionResult<string>> CreateAsync(string itemCreationDto)
    {
        try
        {
            string? item = await _itemLogic.CreateItem(itemCreationDto);
            if (item != null) 
                return Created($"/Items/{item.Length}", item);
            throw new Exception("Error creating an Item!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ICollection<string>>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            string? item  = await _itemLogic.GetItemById(id);
            return Ok(item);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<string>>> GetAsync()
    {
        try
        {
            ICollection<string?> items = await _itemLogic.GetItems();
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}