using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public interface IAdminLogic {
    Task<ICollection<User?>> ListSellersAsync();
    Task AuthorizeSellerAsync(int id, bool authorizationState);
}