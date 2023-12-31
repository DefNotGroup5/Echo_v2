namespace Domain.Shopping.DTOs;

public class UserCreationDto
{
    public int Id { get; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public int PostalCode { get; set; }
    public string Country { get; set; }
    public bool IsSeller { get; set; }
    public bool IsAdmin { get; set; }
    
    public UserCreationDto(string email, string firstName, string lastName, string password, 
        string address, string city, int postalCode, string country, bool isSeller, bool isAdmin)
    {
        Id = 0;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        Password = password;
        Address = address;
        City = city;
        PostalCode = postalCode;
        Country = country;
        IsSeller = isSeller;
        IsAdmin = isAdmin;
    }
}