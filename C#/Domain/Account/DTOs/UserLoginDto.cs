namespace Domain.Account.DTOs;

public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }

    public UserLoginDto(string email, string password)
    {
        Email = email;
        Password = password;
    }
}