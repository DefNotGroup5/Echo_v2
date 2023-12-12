using Domain.Account.DTOs;
using Domain.Shopping.Models;

namespace Application.Account.LogicInterfaces;

public interface ISupportLogic
{
    public Task<Message?> RequestSupportAsync(MessageRequestDto dto);
    public Task ProvideSupportAsync(MessageResponseDto dto);
    public Task<ICollection<Message>?> GetAllAsync();
}