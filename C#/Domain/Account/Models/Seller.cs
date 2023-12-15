using System.Text.Json.Serialization;
using Domain.Shopping.Models;

namespace Domain.Account.Models;
[Serializable]
public class Seller : User
{
    [JsonInclude]
    public bool IsAuthorized { get; set; }
    public Seller(string email, string password) : base(email, password)
    {
        IsAuthorized = false;
        Email = email;
        Password = password;
    }
}