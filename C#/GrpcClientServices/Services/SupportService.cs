using Domain.Account.DTOs;
using Domain.Shopping.Models;
using Grpc.Core;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class SupportService : GrpcClientServices.SupportService.SupportServiceClient
{
    private readonly GrpcChannel _channel;
    
    public SupportService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }

    public async Task<Message?> RequestSupportAsync(Message message)
    {
        try
        {
            var client = new GrpcClientServices.SupportService.SupportServiceClient(_channel);
            var reply = await client.RequestSupportAsync(new SupportRequest()
            { 
                CustomerId = message.CustomerId,
                Message = message.Request
            });
            GrpcMessage grpcMessage = reply.Message;
            Message messageToReturn = new Message(grpcMessage.CustomerId, grpcMessage.Message)
            {
                Id = grpcMessage.Id
            };
            return messageToReturn;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
    
    public async Task ProvideSupportAsync(Message message)
    {
        try
        {
            var client = new GrpcClientServices.SupportService.SupportServiceClient(_channel);
            var reply = await client.ProvideSupportAsync(new ProvideSupportRequest()
            { 
                MessageId = message.Id,
                Response = message.Response
            });
            Console.WriteLine(reply.Result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<ICollection<Message>?> GetAllAsync()
    {
        try
        {
            var client = new GrpcClientServices.SupportService.SupportServiceClient(_channel);
            var reply = await client.GetAllAsync(new GetAllRequest());
            ICollection<Message> messages = new List<Message>();
            foreach (var grpcMessage in reply.Messages)
            {
                Message message = new Message(grpcMessage.CustomerId, grpcMessage.Message)
                {
                    Response = grpcMessage.Response,
                    Answered = grpcMessage.IsAnswered,
                    Id = grpcMessage.Id
                };
                messages.Add(message);
            }

            return messages;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }
}