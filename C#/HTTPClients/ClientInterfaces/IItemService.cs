namespace HTTPClients.ClientInterfaces;

public interface IItemService
{
    public Task<string?> CreateAsync(string itemCreationDto);
    public Task<string?> GetById(int id);
    public Task<ICollection<string?>> GetAsync();
}