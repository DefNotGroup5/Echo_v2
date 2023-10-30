using Domain.Account.Models;

namespace gRPCClients;

public class DataContainer 
{
    public ICollection<User>? Users { get; set; }
}