using Domain.Account.Models;
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

        public async Task AuthorizeSellerAsync(int id, bool authorizationState)
        {
            try
            {
                var client = new GrpcClientServices.AdminService.AdminServiceClient(_channel);
                var reply = await client.AuthorizeSellerAsync(new ChangeSellerAuthorizationRequest()
                {
                    Id = id,
                    AuthorizationState = authorizationState
                });
                Console.WriteLine(reply);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public async Task<ICollection<User?>> ListSellersAsync()
        {
            try
            {
                var client = new GrpcClientServices.AdminService.AdminServiceClient(_channel);
                var reply = await client.ListSellersAsync(new ListUsersRequest());
                ICollection<User?> users = new List<User?>();
                foreach (GrpcUser user in reply.Users)
                {
                    users.Add(UsersService.GenerateUser(user));
                }
                return users;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new List<User?>();
        }
    }
}