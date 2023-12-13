using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IReviewService
{
    Task<Review> AddReviewAsync(Review review);
    Task<IEnumerable<Review>> GetReviewsByItemAsync(int itemId);
    Task<IEnumerable<Review>> GetReviewsByUserAsync(int userId);
}