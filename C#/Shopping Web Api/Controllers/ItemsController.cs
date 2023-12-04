using Application.Shopping.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
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
    public async Task<ActionResult<Item>> CreateAsync(ItemCreationDto dto)
    {
        try
        {
            Item? item = await _itemLogic.CreateItem(dto);
            if (item != null) 
                return Created($"/Items/{item.Id}", item);
            throw new Exception("Error creating an Item!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ICollection<Item>>> GetByIdAsync([FromRoute] int id)
    {
        try
        {
            Item? item  = await _itemLogic.GetItemById(id);
            return Ok(item);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Item>>> GetAsync()
    {
        try
        {
            ICollection<Item?> items = await _itemLogic.GetItems();
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}