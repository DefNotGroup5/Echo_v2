using Domain.Shopping.Models;

namespace Domain.Account.DTOs;

public class UserTransferDto
{
    public User? User { get; set; }
    public bool IsSeller { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsAuthorized { get; set; }
}