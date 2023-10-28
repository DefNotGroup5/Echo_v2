using Application.Account.LogicInterfaces;
using Domain.Account.DTOs;
using Microsoft.AspNetCore.Http.HttpResults;    
using Microsoft.AspNetCore.Mvc;

namespace Account_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserLogic _userLogic;

    public UsersController(IUserLogic userLogic)
    {
        _userLogic = userLogic;
    }

    [HttpPatch("Login")]
    public async Task<ActionResult> Login(UserLoginDto userLoginDto)
    {
        try
        {
            await _userLogic.Login(userLoginDto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}