using Application.Shopping.LogicInterfaces;
using Domain.Shopping.Models;
using GrpcClientServices;
using ReviewService = GrpcClientServices.Services.ReviewService;

namespace Application.Shopping.Logic;

public class ReviewLogic : IReviewLogic
{
    private readonly ReviewService _reviewService;

    public ReviewLogic(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        
        if (review.Rating < 1 || review.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        
        return await _reviewService.AddReviewAsync(review);
    }
    
    public async Task<IEnumerable<Review>> GetReviewsByItemAsync(int itemId)
    {
        var response = await _reviewService.GetReviewsByItemAsync(new GetReviewsByItemRequest { ItemId = itemId });
        var reviews = new List<Review>();
        foreach (var grpcReview in response.Reviews)
        {
            reviews.Add(new Review(grpcReview.Id, grpcReview.UserId, grpcReview.ItemId, grpcReview.Rating, grpcReview.Comment));
        }
        return reviews;
    }

    public async Task<IEnumerable<Review>> GetReviewsByUserAsync(int userId)
    {
        var response = await _reviewService.GetReviewsByUserAsync(new GetReviewsByUserRequest { UserId = userId });
        var reviews = new List<Review>();
        foreach (var grpcReview in response.Reviews)
        {
            reviews.Add(new Review(grpcReview.Id, grpcReview.UserId, grpcReview.ItemId, grpcReview.Rating, grpcReview.Comment));
        }
        return reviews;
    }
}