using Domain.Account.Models;

namespace GrpcClientServices.Services;
using Grpc.Net.Client;
public class UsersService : GrpcClientServices.UsersService.UsersServiceClient
{
    private readonly GrpcChannel _channel;

    public UsersService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }
    public async Task AddAsync(User user)
    {
        try
        {
            
            var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
            var reply = await client.AddAsync(new AddRequest()
            {
                Id = user.Id,
                Email = user.Email,
                Address = user.Address,
                City = user.City,
                Country = user.Country,
                FirstName = user.FirstName,
                IsLoggedIn = user.IsLoggedIn,
                IsSeller = user.IsSeller,
                LastName = user.LastName,
                Password = user.Password,
                PostalCode = (int)user.PostalCode
            });
            Console.WriteLine(reply);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task UpdateAsync(User user)
    {
        var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
        var reply = await client.UpdateAsync(new UpdateRequest()
        {
            Id = user.Id,
            Email = user.Email,
            Address = user.Address,
            City = user.City,
            Country = user.Country,
            FirstName = user.FirstName,
            IsLoggedIn = user.IsLoggedIn,
            IsSeller = user.IsSeller,
            LastName = user.LastName,
            Password = user.Password,
            PostalCode = (int)user.PostalCode
        });
    }
}