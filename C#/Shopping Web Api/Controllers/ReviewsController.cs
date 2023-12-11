using Application.Shopping.LogicInterfaces;
using Domain.Account.DTOs;
using Domain.Account.Models;
using Domain.Shopping.DTOs;
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

 //   [HttpPost]
}