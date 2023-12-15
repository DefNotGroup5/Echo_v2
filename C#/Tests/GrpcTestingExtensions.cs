using Grpc.Core;
using Grpc.Core.Testing;
using System.Threading.Tasks;

public static class GrpcTestingExtensions
{
    public static AsyncUnaryCall<TResponse> ToAsyncUnaryCall<TResponse>(this Task<TResponse> task)
    {
        return TestCalls.AsyncUnaryCall(task, Task.FromResult(new Metadata()), () => Status.DefaultSuccess, () => new Metadata(), () => { });
    }
}