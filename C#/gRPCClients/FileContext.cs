using System.Text.Json;
using Domain.Account.Models;

namespace gRPCClients;

public class FileContext
{
    private const string FilePath = "data.json";
    private DataContainer? _dataContainer;

    public ICollection<User>? Users
    {
        get
        {
            LazyLoadData();
            return _dataContainer?.Users;
        }
    }
    
    private void LazyLoadData()
    {
        if (_dataContainer == null)
        {
            LoadData();
        }
    }
    
    private void LoadData()
    {
        if (_dataContainer != null) return;

        if (!File.Exists(FilePath))
        {
            _dataContainer = new()
            {
                Users = new List<User>()
            };
            return;
        }
        string content = File.ReadAllText(FilePath);
        _dataContainer = JsonSerializer.Deserialize<DataContainer>(content);
    }
    
    public void SaveChanges()
    {
        string serialized = JsonSerializer.Serialize(_dataContainer, new JsonSerializerOptions
        {
            WriteIndented = true
        });
        File.WriteAllText(FilePath, serialized);
        _dataContainer = null;
    } 
}