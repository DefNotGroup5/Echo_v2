using Domain.Account.Models;

namespace Application.Account.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByEmailAsync(string email);
    Task Login(User user);
    Task Logout(User user);
}