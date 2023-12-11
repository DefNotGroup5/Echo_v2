using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "IsSeller")]
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
            Item? item = await _itemLogic.CreateItemAsync(dto);
            if (item != null) return Created($"/Items/{item.Id}", item);
            return StatusCode(500, "ERROR! Item was null!");
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
            Item? item  = await _itemLogic.GetItemByIdAsync(id);
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
            ICollection<Item?> items = await _itemLogic.GetItemsAsync();
            return Ok(items);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}