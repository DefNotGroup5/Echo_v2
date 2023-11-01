using Domain.Account.Models;
using Grpc.Net.Client;
using Grpc.Core;
using gRPCClients.Account.GrpcClientInterfaces;

namespace gRPCClients.Account;

public class GrpcUserClient : IGrpcUser
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress("http://localhost:3030");
    public Task AddUser(User user)
    {
        
        throw new NotImplementedException();
    }

    public Task UpdateUser(User user)
    {
        throw new NotImplementedException();
    }
}