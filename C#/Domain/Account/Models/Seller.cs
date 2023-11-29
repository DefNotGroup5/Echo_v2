namespace Domain.Account.Models;

public class Seller : User
{
    public Seller(string email, string password) : base(email, password)
    {
        Email = email;
        Password = password;
    }
}