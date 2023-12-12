using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IReviewLogic
{
    Task<Review> AddReviewAsync(Review review);
    Task<IEnumerable<Review>> GetReviewsByItemAsync(int itemId);
    Task<IEnumerable<Review>> GetReviewsByUserAsync(int userId);
}