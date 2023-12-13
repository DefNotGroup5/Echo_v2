namespace Domain.Shopping.Models;

public class Review
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; }

    public Review(int id, int userId, int itemId, int rating, string comment)
    {
        Id = id;
        UserId = userId;
        ItemId = itemId;
        Rating = rating;
        Comment = comment;
    }

    public Review()
    {
        throw new NotImplementedException();
    }
}