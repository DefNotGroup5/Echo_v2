using Application.Account.DaoInterfaces;
using Domain.Account.Models;

namespace gRPCClients.Account;

public class UserFileDao : IUserDao
{
    private readonly FileContext _context;

    public UserFileDao(FileContext context)
    {
        _context = context;
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (_context.Users != null && _context.Users.Any())
        {
            userId = _context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;
        
        _context.Users?.Add(user);
        _context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        User? existing =
            _context.Users?.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(existing);
    }

    public Task Login(User user)
    {
        User userToUpdate = user;
        userToUpdate.IsLoggedIn = true;
        _context.Users?.Remove(user);
        _context.Users?.Add(userToUpdate);
        _context.SaveChanges();
        return Task.CompletedTask;
    }

    public Task Logout(User user)
    {
        User userToUpdate = user;
        userToUpdate.IsLoggedIn = false;
        _context.Users?.Remove(user);
        _context.Users?.Add(userToUpdate);
        _context.SaveChanges();
        return Task.CompletedTask;
    }
}