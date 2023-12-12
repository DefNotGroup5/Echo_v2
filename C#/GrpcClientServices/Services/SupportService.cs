using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class SupportService : GrpcClientServices.SupportService.SupportServiceClient
{
    private readonly GrpcChannel _channel;
    
    public SupportService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }

}