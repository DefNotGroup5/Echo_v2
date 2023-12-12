using System.Collections;
using Application.Account.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Mvc;

namespace Account_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SupportController : ControllerBase
{
    private readonly ISupportLogic _supportLogic;

    public SupportController(ISupportLogic supportLogic)
    {
        _supportLogic = supportLogic;
    }

    [HttpPost]
    public async Task<ActionResult> RequestSupportAsync([FromBody] MessageRequestDto dto)
    {
        try
        {
            Message? message = await _supportLogic.RequestSupportAsync(dto);
            if (message != null)
                return Created($"/Support/{message.Id}", message);
            return StatusCode(500, "ERROR! Item was null!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> ProvideSupportAsync([FromBody] MessageResponseDto dto)
    {
        try
        {
            await _supportLogic.ProvideSupportAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<ICollection<Message>>> GetAllAsync()
    {
        try
        {
            ICollection<Message>? messages = await _supportLogic.GetAllAsync();
            return Ok(messages);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}