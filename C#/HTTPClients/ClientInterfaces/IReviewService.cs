using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace HTTPClients.ClientInterfaces;

public interface IReviewService
{
    Task<Review> AddReviewAsync(ReviewCreationDto dto);
    Task<ICollection<Review>> GetReviewsByItemAsync(int itemId);
    Task<ICollection<Review>> GetReviewsByUserAsync(int userId);
}