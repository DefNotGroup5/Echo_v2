using Domain.Account.Models;
using Domain.Shopping.Models;

namespace Application.Shopping.LogicInterfaces;

public interface IReviewLogic
{
    Task<Review> AddReviewAsync(Review review);
}