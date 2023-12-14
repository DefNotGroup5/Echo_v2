using Moq;
using Application.Account.LogicInterfaces;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Account_Web_Api.Controllers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Tests;

public class RegisterUserTest
{
    private Mock<IUserLogic> _userLogicMock;
    private UsersController _controller;
    private Mock<IConfiguration> _configMock;

    [SetUp]
    public void Setup()
    {
        _userLogicMock = new Mock<IUserLogic>();
        _configMock = new Mock<IConfiguration>();
        _controller = new UsersController(_configMock.Object, _userLogicMock.Object);
    }

    [Test]
    public async Task Register_WithValidUser_ReturnsCreatedUser()
    {
        // Arrange
        var userCreationDto = new UserCreationDto("john.doe@example.com", "John", "Doe", "password123", 
            "123 Main St", "Anytown", 12345, "USA", false, false);
        var user = new User("john.doe@example.com", "password123");
        _userLogicMock.Setup(ul => ul.Register(It.IsAny<UserCreationDto>())).ReturnsAsync(user);

        // Act
        var actionResult = await _controller.Register(userCreationDto);

        // Assert
        Assert.That(actionResult, Is.Not.Null);

        var objectResult = actionResult.Result as ObjectResult;
        Assert.That(objectResult, Is.Not.Null);
        Assert.That(objectResult.StatusCode, Is.EqualTo(201)); // Assuming 201 is the status code for creation

        var returnedUser = objectResult.Value as User;
        Assert.That(returnedUser, Is.Not.Null);
        Assert.That(returnedUser.Email, Is.EqualTo(user.Email));
    }
}