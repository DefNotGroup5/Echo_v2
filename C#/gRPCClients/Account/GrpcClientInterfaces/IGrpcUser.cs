using Domain.Account.Models;

namespace gRPCClients.Account.GrpcClientInterfaces;

public interface IGrpcUser
{
    public Task AddUser(User user);
    public Task UpdateUser(User user);
}