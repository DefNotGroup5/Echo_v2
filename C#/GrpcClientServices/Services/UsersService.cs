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
            GrpcUser userToAdd = GenerateGrpcUser(user);
            var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
            var reply = await client.AddAsync(new AddRequest()
            {
                User = userToAdd
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
        try
        {
            GrpcUser userToUpdate = GenerateGrpcUser(user);
            var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
            var reply = await client.UpdateAsync(new UpdateRequest()
            {
                User = userToUpdate
            });
            Console.WriteLine(reply);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        try
        {
            var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
            var reply = await client.GetByEmailAsync(new GetByEmailRequest()
            {
                Email = email
            });
            User user = GenerateUser(reply.User);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
    
    public async Task<User?> GetByIdAsync(int id)
    {
        try
        {
            var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
            var reply = await client.GetByIdAsync(new GetByIdRequest()
            {
                Id = id
            });
            User user = GenerateUser(reply.User);
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    private User GenerateUser(GrpcUser user)
    {
        User generatedUser = new User(user.Email, user.Password)
        {
            Id = user.Id,
            Address = user.Address,
            City = user.City,
            Country = user.Country,
            FirstName = user.FirstName,
            IsSeller = user.IsSeller,
            LastName = user.LastName,
            PostalCode = user.PostalCode
        };
        return generatedUser;
    }

    private GrpcUser GenerateGrpcUser(User user)
    {
        GrpcUser generatedGrpcUser = new GrpcUser()
        {
            Id = user.Id,
            Email = user.Email,
            Address = user.Address,
            City = user.City,
            Country = user.Country,
            FirstName = user.FirstName,
            IsSeller = user.IsSeller,
            LastName = user.LastName,
            Password = user.Password,
            PostalCode = (int)user.PostalCode
        };
        return generatedGrpcUser;
    }
}