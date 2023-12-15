using Moq;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;
using Grpc.Net.Client;
using Grpc.Core;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using GrpcClientServices;
using ItemService = GrpcClientServices.Services.ItemService;
using OrderService = GrpcClientServices.Services.OrderService;

namespace Tests;

[TestFixture]
public class ManageOrderTests
{
    private Mock<GrpcClientServices.OrderService.OrderServiceClient> _mockGrpcClient;
    private OrderService _orderService;
    private Mock<ItemService> _mockItemService;

    [SetUp]
    public void Setup()
    {
        _mockGrpcClient = new Mock<GrpcClientServices.OrderService.OrderServiceClient>();
        _mockItemService = new Mock<ItemService>();
        _orderService = new OrderService(_mockItemService.Object);
    }
    
    [Test]
    public async Task CreateOrderAsync_WithValidData_ReturnsOrder()
    {
        // Given
        var orderCreationDto = new OrderCreationDto
        {
            CustomerId = 1,
            ItemIds = new List<int> { 1, 2 }
        };
        var mockOrderResponse = new CreateOrderResponse
        {
            // Assuming the response structure includes these fields
            OrderItem = new GrpcOrderItem()
            {
                CustomerId = orderCreationDto.CustomerId,
                OrderId = "order123",
                Status = "Pending",
                TotalPrice = 100.0,
            }
        };

        var mockAsyncUnaryCall = Task.FromResult(mockOrderResponse).ToAsyncUnaryCall();

        _mockGrpcClient.Setup(client => client.CreateOrderAsync(It.IsAny<CreateOrderRequest>(), null, null, default))
            .Returns(mockAsyncUnaryCall);

        // Act
        var resultOrder = await _orderService.CreateOrderAsync(orderCreationDto);
        
        // Assert
        Assert.IsNotNull(resultOrder);
        Assert.AreEqual("order123", resultOrder.OrderId);
        Assert.AreEqual("Pending", resultOrder.Status);
        Assert.AreEqual(100.0, resultOrder.TotalPrice);
    }
}