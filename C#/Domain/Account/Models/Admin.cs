namespace Domain.Shopping.Models;
[Serializable]
public class Admin : User
{
    public Admin(string email, string password) : base(email, password)
    {
        Email = email;
        Password = password;
    }
}
