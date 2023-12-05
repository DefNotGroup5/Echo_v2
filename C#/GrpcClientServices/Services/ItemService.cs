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

    public async Task AddAsync(Item item)
    {
        try
        {
            GrpcItem itemToAdd = GenerateGrpcItem(item);
            var client = new GrpcClientServices.ItemService.ItemServiceClient(_channel);
            var reply = await client.AddItemAsync(new AddItemRequest()
            { 
               GrpcItem = itemToAdd
            });
            
            Console.WriteLine(reply);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
    }

    public async Task<Item?> GetItemById(int id, int sellerId)
    {
        try
        {
            var grpcClient = new GrpcClientServices.ItemService.ItemServiceClient(_channel);
            var reply = await grpcClient.GetItemById(new GetItemByIdRequest 
            {
                ItemId = id, SellerId =sellerId
                
            });
            Item? item = GenerateItem(reply.Item);
            return item;

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
                Id = item.ItemId,
                SellerId = item.SellerId,
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
            ItemId = item.Id, 
            SellerId = item.SellerId,  
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