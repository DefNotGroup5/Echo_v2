namespace Domain.Account.Models;

public class Seller : User
{
    public bool IsAuthorized { get; set; }
    public bool IsSeller { get; set; }
    public Seller(string email, string password) : base(email, password)
    {
        IsAuthorized = false;
        Email = email;
        Password = password;
        IsSeller = true;
    }
}