namespace Domain.Account.Models;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public long PostalCode { get; set; }
    public string Country { get; set; }
    public bool IsAdmin { get; set; }

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
