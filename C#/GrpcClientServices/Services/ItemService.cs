using Domain.Account.Models;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class ItemService : GrpcClientServices.
{
    
    private readonly GrpcChannel _channel;
    
    public ItemService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }
    
    public Task<string?> CreateItem(string itemCreationDto)
    {
        try
        {
            GrpcItem itemGrpc = new GrpcItem();
            itemGrpc = GenerateGrpcItem()
            var reply = await ClientCertificateOption.
            
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

        return null;
    }

    public Task<string?> GetItemById(int id)
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return 
    }
    
    
    public async Task<ICollection<string?>> AddAsync()
    {
        
        try
        {
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }
    
    private Item? GenerateItem(GrpcItem item, GrpcUser user)
    {
        Item? generatedItem = null;
        if (user.IsSeller)
        {
            generatedItem = new Item()
            {
                Id = item.Id,
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                Description = item.Description,
                Price = item.Price,
                Quantity = item.Quantity,
                Stock = item.Stock,
            };
        }
        
        return generatedItem;
    }

    private GrpcUser GenerateGrpcItem(Item item, User user)
    {
        bool isSeller = user is Seller;
        
        GrpcItem generatedGrpcItem = new GrpcItem()
        {
            Id = item.Id,
            Name = item.Name,
            ImageUrl = item.ImageUrl,
            Description = item.Description,
            Price = item.Price,
            Quantity = item.Quantity,
            Stock = item.Stock,
            
        };
        return generatedGrpcItem;
    }
}