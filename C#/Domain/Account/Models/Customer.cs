namespace Domain.Shopping.Models;

public class Customer : User
{
    public Customer(string email, string password) : base(email, password)
    {
        Email = email;
        Password = password;
    }
}