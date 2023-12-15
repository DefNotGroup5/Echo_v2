using Application.Shopping.LogicInterfaces;

using Domain.Shopping.DTOs;

using Domain.Shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewLogic _reviewLogic;

    public ReviewsController(IReviewLogic reviewLogic)
    {
        _reviewLogic = reviewLogic;
    }

    [HttpPost]
    public async Task<IActionResult> AddReviewAsync([FromBody] ReviewCreationDto dto)
    {
        try
        {
            var addedReview = await _reviewLogic.AddReviewAsync(dto);
            return Created($"/Reviews/{addedReview.Id}", addedReview);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("by-item/{itemId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByItemAsync(int itemId)
    {
        try
        {
            var reviews = await _reviewLogic.GetReviewsByItemAsync(itemId);
            return Ok(reviews);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("by-user/{userId}")]
    public async Task<ActionResult<IEnumerable<Review>>> GetReviewsByUserAsync(int userId)
    {
        try
        {
            var reviews = await _reviewLogic.GetReviewsByUserAsync(userId);
            return Ok(reviews);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}