using Application.Shopping.LogicInterfaces;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Policy = "IsCustomer")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewLogic _reviewLogic;

    public ReviewsController(IReviewLogic reviewLogic)
    {
        _reviewLogic = reviewLogic;
    }

    [HttpPost]
    public async Task<IActionResult> AddReviewAsync([FromBody] Review review)
    {
        try
        {
            // Call your logic layer to add the review
            var addedReview = await _reviewLogic.AddReviewAsync(review);
            if (addedReview != null)
            {
                return CreatedAtAction(nameof(GetReviewById), new { id = addedReview.Id }, addedReview);
            }
            return BadRequest("Failed to add review.");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, "Internal Server Error");
        }
    }
    
}