namespace Domain.Shopping.Models;

public class Admin : User
{
    public Admin(string email, string password) : base(email, password)
    {
        Email = email;
        Password = password;
    }
}
