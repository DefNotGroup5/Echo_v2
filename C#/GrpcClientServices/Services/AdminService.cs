using Grpc.Net.Client;

namespace GrpcClientServices.Services
{
    public class AdminService : GrpcClientServices.AdminService.AdminServiceClient
    {
        private readonly GrpcChannel _channel;

        public AdminService()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:3030");
        }

        
        public async Task<AddRequest.AuthorizeSellerResponse> AuthorizeSellerAsync(int userId, bool isAuthorized)
        {
            try
            {
                var client = new GrpcClientServices.AdminService.AdminServiceClient(_channel);
                var reply = await client.AuthorizeSellerAsync(new AddRequest.AuthorizeSellerRequest()
                {
                    Id = userId,
                    IsAuthorized = isAuthorized
                });
                return reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        
        public async Task<AddRequest.ListUsersResponse> ListSellersAsync()
        {
            try
            {
                var client = new GrpcClientServices.AdminService.AdminServiceClient(_channel);
                var reply = await client.ListSellersAsync(new Google.Protobuf.WellKnownTypes.Empty());
                return reply;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
    }
}