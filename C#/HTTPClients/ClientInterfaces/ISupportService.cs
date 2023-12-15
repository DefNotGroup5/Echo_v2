using Domain.Account.DTOs;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface ISupportService
{
    public Task<Message?> RequestSupport(MessageRequestDto dto);
    public Task ProvideSupport(MessageResponseDto dto);
    public Task<ICollection<Message>> GetAll();
    public Task<ICollection<Message>> GetAllByIsAnswered(bool isAnswered);
}