namespace Domain.Account.DTOs;

public class UserCreationDto
{
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
    
    public UserCreationDto(string username, string email, string firstName, string lastName, string password, 
        string address, string city, long postalCode, string country, bool isSeller)
    {
        Username = username;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Address = address;
        City = city;
        PostalCode = postalCode;
        Country = country;
        IsSeller = isSeller;
    }
}