using Domain.Shopping.Models;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class ItemService : GrpcClientServices.ItemService.ItemServiceClient
{
    private readonly GrpcChannel _channel;
    
    public ItemService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }

    public async Task<Item?> AddItemAsync(Item item)
    {
        try
        {
            GrpcItem itemToAdd = GenerateGrpcItem(item);
            var client = new GrpcClientServices.ItemService.ItemServiceClient(_channel);
            var reply = await client.AddItemAsync(new AddItemRequest()
            { 
               Item = itemToAdd
            });
            Item? itemToReturn = GenerateItem(reply.Item);
            Console.WriteLine(reply);
            return itemToReturn;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return null;
    }

    public async Task<Item?> GetItemByIdAsync(int id)
    {
        try
        {
            var grpcClient = new GrpcClientServices.ItemService.ItemServiceClient(_channel);
            var reply = await grpcClient.GetItemByIdAsync(new GetItemByIdRequest 
            {
                Id = id
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

    public async Task<ICollection<Item?>> GetAllItemsAsync()
    {
        try
        {
            var grpcClient = new GrpcClientServices.ItemService.ItemServiceClient(_channel);
            var reply = await grpcClient.GetAllItemsAsync(new GetAllItemsRequest());
            ICollection<Item?> items = new List<Item?>();
            foreach (var item in reply.Items)
            {
                items.Add(GenerateItem(item));
            }
            return items;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new List<Item?>();
    }

    private Item? GenerateItem(GrpcItem item)
    {
        Item? generatedItem = null;
        
        
            generatedItem = new Item(item.Name, item.ImageUrl, item.Description)
            {
                Id = item.ItemId,
                SellerId = item.SellerId,
                Price = item.Price,
                Quantity = item.Quantity,
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

        };
        return generatedGrpcItem;
    }
}