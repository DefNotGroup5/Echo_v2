using Domain.Account.Models;

namespace Application.Account.DaoInterfaces;

public interface IUserDao
{
    Task<User> CreateAsync(User user);
    Task<User?> GetByUsernameAsync(string username);
    Task Login(User user);
    Task Logout(User user);
}