﻿using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistLogic _wishlistLogic;

    public WishlistController(IWishlistLogic wishlistLogic)
    {
        _wishlistLogic = wishlistLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Wishlist>> CreateAsync(WishlistCreationDto dto)
    {
        try
        {
            Wishlist? wishlist = await _wishlistLogic.CreateWishlistItemAsync(dto);
            if (wishlist != null) return Created($"/Wishlist/{wishlist.Id}", wishlist);
            return StatusCode(500, "ERROR! Wishlist is null");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("by-user/{userId:int}")]
    public async Task<ActionResult<ICollection<Wishlist>>> GetByUserIdAsync([FromRoute] int userId)
    {
        try
        {
            ICollection<Wishlist?> wishlist = await _wishlistLogic.GetWishlistByUserAsync(userId);
            return Ok(wishlist);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> RemoveWishlist([FromRoute] int id)
    {
        try
        {
            await _wishlistLogic.RemoveWishlist(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}