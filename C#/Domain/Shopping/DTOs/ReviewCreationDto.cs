namespace Domain.Shopping.DTOs;

    public class ReviewCreationDto
    {
        public int UserId { get; set; }
        public int ItemId { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
    }
