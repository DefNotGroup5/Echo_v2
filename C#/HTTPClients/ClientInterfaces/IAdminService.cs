using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IAdminService
{
    Task<IEnumerable<User>> ListSellersAsync();
    Task AuthorizeSellerAsync(int userId, bool isAuthorized);
    Task DeleteSellerAsync(int id);
}