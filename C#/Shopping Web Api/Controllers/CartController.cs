using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CartController : ControllerBase
{
    private readonly IShoppingCartLogic _shoppingCartLogic;

    public CartController(IShoppingCartLogic shoppingCartLogic)
    {
        _shoppingCartLogic = shoppingCartLogic;
    }

    [HttpPost]
    public async Task<ActionResult<CartItem>> AddAsync([FromBody] CartItemCreationDto dto)
    {
        try
        {
            CartItem? item = await _shoppingCartLogic.AddAsync(dto);
            if (item != null)
            {
                return Created($"/Cart/{item.Id}", item);
            }

            throw new Exception("Error adding to cart!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<ICollection<Item>>> GetByCustomer([FromRoute] int id)
    {
        try
        { 
            return Ok(await _shoppingCartLogic.GetAllByCustomerId(id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}