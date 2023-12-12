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

    }
}