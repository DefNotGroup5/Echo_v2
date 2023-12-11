namespace Domain.Account.Models;

public class Review
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }
}