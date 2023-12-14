using System.Collections;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IReviewLogic
{
    Task<Review> AddReviewAsync(ReviewCreationDto dto);
    Task<ICollection<Review>> GetReviewsByItemAsync(int itemId);
    Task<ICollection<Review>> GetReviewsByUserAsync(int userId);
}