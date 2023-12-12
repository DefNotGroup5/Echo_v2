using Grpc.Net.Client;
using Domain.Shopping.Models;

namespace GrpcClientServices.Services
{
    public class OrderService : GrpcClientServices.OrderService.OrderServiceClient
    {
        private readonly GrpcChannel _channel;
        public OrderService()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:3030");
        }

        public async Task<Order?> AddOrderAsync(Order order)
        {
            try
            {
                GrpcOrder grpcOrder = new GrpcOrder();
                grpcOrder.OrderId = order.Id;
                grpcOrder.CustomerId = order.CustomerId;
                grpcOrder.TotalCost = order.TotalPrice;
                
                var client = new GrpcClientServices.OrderService.OrderServiceClient(_channel);
                var reply = await client.CreateOrderAsync(new CreateOrderRequest
                {
                    Order = grpcOrder
                });

                return new Order(reply.Order.CustomerId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }
    }
}