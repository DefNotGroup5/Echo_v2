namespace gRPCClients.Account;

public class GrpcItemClient : IGrpcItem
{
    private readonly GrpcChannel _channel = GrpcChannel.ForAddress("http://localhost:3030");
    
    
}