namespace gRPCClients.Account;

public class ItemFileDao : IItemDao
{
    private readonly FileContext _context;
    
    public ItemFileDao(FileContext context)
    {
        _context = context;
    }
    
    public Task<Item> CreateAsync(Item item)
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
    
}