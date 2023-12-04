using Domain.Account.Models;

namespace Application.Account.LogicInterfaces;

public interface IAdminLogic {
    Task<IEnumerable<User>> ListSellersAsync();
    Task AuthorizeSellerAsync(int userId, bool isAuthorized);
}