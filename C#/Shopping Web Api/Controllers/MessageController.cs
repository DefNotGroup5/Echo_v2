using Application.Shopping.LogicInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

public class MessageController : ControllerBase
{
    private readonly IMessageLogic _messageLogic;
    
    public MessageController(IMessageLogic messageLogic)
    {
        _messageLogic = messageLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync()
    {
        try
        {
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}