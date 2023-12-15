using System.Collections;
using Application.Shopping.LogicInterfaces;
using Domain.Shopping.DTOs;
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

    public async Task<Review> AddReviewAsync(ReviewCreationDto dto)
    {
        ICollection<Review> reviews = await GetReviewsByUserAsync(dto.UserId);
        foreach (var review in reviews)
        {
            if (review.Id == dto.ItemId)
            {
                throw new Exception("User cannot review twice an item.");
            }
        }
        if (dto.Rating < 1 || dto.Rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5.");
        return await _reviewService.AddReviewAsync(dto);
    }
    
    public async Task<ICollection<Review>> GetReviewsByItemAsync(int itemId)
    {
        ICollection<Review> initialReviews = await _reviewService.GetAllAsync();
        ICollection<Review> reviews = new List<Review>();
        foreach (var review in initialReviews)
        {
            if (review.ItemId == itemId)
            {
                reviews.Add(review);
            }
        }
        return reviews;
    }

    public async Task<ICollection<Review>> GetReviewsByUserAsync(int userId)
    {
        ICollection<Review> initialReviews = await _reviewService.GetAllAsync();
        ICollection<Review> reviews = new List<Review>();
        foreach (var review in initialReviews)
        {
            if (review.UserId == userId)
            {
                reviews.Add(review);
            }
        }
        return reviews;
    }
}