namespace Domain.Account.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public long PostalCode { get; set; }
    public string Country { get; set; }
    public bool IsSeller { get; set; }
    public bool IsLoggedIn { get; set; }

    public User(string username, string password)
    {
        Username = username;
        Password = password;
        IsLoggedIn = false;
    }
}
