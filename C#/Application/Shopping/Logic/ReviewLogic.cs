using Application.Shopping.LogicInterfaces;
using Domain.Account.Models;
using Domain.Shopping.Models;
using GrpcClientServices.Services;

namespace Application.Shopping.Logic;

public class ReviewLogic : IReviewLogic
{
    private readonly ReviewService _reviewService;

    public ReviewLogic(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    /*public async Task<Review> AddReviewAsync(Review review)
    {
        
        if (review.Rating < 1 || review.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");

        
        return await _reviewService.AddReviewAsync(review);
    }*/
}