using System.Collections;
using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Shopping_Web_Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderLogic _orderLogic;
    
    public OrderController(IOrderLogic orderLogic)
    {
        _orderLogic = orderLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Order>> CreateAsync(OrderCreationDto dto)
    {
        try
        {
            Order? order = await _orderLogic.CreateOrder(dto);
            if (order != null)
            {
                return Created($"Order/{order.OrderId}",order);
            }
            return StatusCode(500, "ERROR! Order was null!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Order>> GetByIdAsync([FromRoute] string id)
    {
        try
        {
            Order? order  = await _orderLogic.GetOrderById(id);
            return Ok(order);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<ICollection<Order>>> GetAsync()
    {
        try
        {
            ICollection<Order?> orders = await _orderLogic.GetOrders();
            return Ok(orders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("by-seller/{sellerId:int}")]
    public async Task<ActionResult<ICollection<Order>>> GetBySellerIdAsync([FromRoute] int sellerId)
    {
        try
        {
            ICollection<Order?> orders = await _orderLogic.GetOrdersBySeller(sellerId);
            return Ok(orders);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
}