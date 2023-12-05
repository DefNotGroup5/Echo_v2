using Domain.Account.Models;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class ItemService : GrpcClientServices.ItemService.ItemServiceClient
{
    private readonly GrpcChannel _channel;
    
    public ItemService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }
    
    public Task<string?> CreateItem(string itemCreationDto, Item item, User user)
    {
        try
        {
            GrpcItem itemGrpc = GenerateGrpcItem(item);
            Item newItem = GenerateItem(itemGrpc);
            Console.WriteLine("A new Item was created");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }

    public async Task<ICollection<string?>> AddAsync(Item item)
    {
        ICollection<string> items = new List<string>();
        
        try
        {
            GrpcItem itemToAdd = GenerateGrpcItem(item);
            var client = new GrpcClientServices.UsersService.UsersServiceClient(_channel);
            var reply = await client.AddAsync(new AddRequest
            { 
               Item = itemToAdd
            });

            foreach (var replyItem in reply.Items)
            {
                items.Add(replyItem);
            }
            Console.WriteLine("Item was added");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return items;
    }

    public Task<string?> GetItemById(int id)
    {
        try
        {
            var grpcClient = new GrpcClientServices.Services.ItemService(_channel);
            var reply = await grpcClient.GetByIdAsync(new GetByIdRequest 
            {
                Id = id
            });
            return item?.ToString();


        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }
    
    
   
    
    private Item? GenerateItem(GrpcItem item)
    {
        Item? generatedItem = null;
        
        
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
        
        
        return generatedItem;
    }

    private GrpcItem GenerateGrpcItem(Item item)
    {
        
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