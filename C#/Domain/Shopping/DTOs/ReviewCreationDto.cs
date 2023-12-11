namespace Domain.Shopping.DTOs;

    public class ReviewCreationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public ReviewCreationDto(int id, int userId, int itemId, int rating, string comment)
        {
            Id = id;
            UserId = userId;
            ItemId = itemId;
            Rating = rating;
            Comment = comment;
        }
    }
