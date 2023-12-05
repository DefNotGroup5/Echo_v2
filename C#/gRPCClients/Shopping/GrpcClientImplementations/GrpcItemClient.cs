namespace gRPCClients.Account;

public class GrpcItemClient : IGrpcItem
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress("http://localhost:3030");

    public Task<string?> CreateItem(string itemCreationDto)
    public Task<string?> GetItemById(int id)

    public Task<ICollection<string?>> GetItems();
    
    
}