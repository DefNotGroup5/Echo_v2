using Microsoft.AspNetCore.Mvc;
using Application.Account.LogicInterfaces;
using System.Threading.Tasks;

namespace Account_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminLogic _adminLogic;

    public AdminController(IAdminLogic adminLogic)
    {
        _adminLogic = adminLogic;
    }
    
    [HttpGet("sellers")]
    public async Task<IActionResult> ListSellers()
    {
        try
        {
            var sellers = await _adminLogic.ListSellersAsync();
            return Ok(sellers);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "An error occurred while listing sellers.");
        }
    }
    
    [HttpPost("authorize-seller/{userId}")]
    public async Task<IActionResult> AuthorizeSeller(int userId, [FromBody] bool isAuthorized)
    {
        try
        {
            await _adminLogic.AuthorizeSellerAsync(userId, isAuthorized);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return StatusCode(500, "An error occurred while authorizing the seller.");
        }
    }
}