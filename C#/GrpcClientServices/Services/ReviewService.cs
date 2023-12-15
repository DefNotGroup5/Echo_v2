using Grpc.Net.Client;
using GrpcClientServices;
using System.Threading.Tasks;
using Domain.Shopping.DTOs;
using Domain.Shopping.Models;

namespace GrpcClientServices.Services
{
    public class ReviewService : GrpcClientServices.ReviewService.ReviewServiceClient
    {
        private readonly GrpcChannel _channel;

        public ReviewService()
        {
            _channel = GrpcChannel.ForAddress("http://localhost:3030");
        }

        public async Task<Review> AddReviewAsync(ReviewCreationDto review)
        {
            try
            {
                var client = new GrpcClientServices.ReviewService.ReviewServiceClient(_channel);
                var reply = await client.CreateReviewAsync(new CreateReviewRequest()
                {
                    CustomerId = review.UserId,
                    Comment = review.Comment,
                    ItemId = review.ItemId,
                    Rating = review.Rating
                });

                return new Review(
                    id: reply.Review.Id,
                    userId: reply.Review.CustomerId,
                    itemId: reply.Review.ItemId,
                    rating: reply.Review.Rating,
                    comment: reply.Review.Comment
                );
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in AddReviewAsync: {e.Message}");
                throw;
            }
        }

        public async Task<ICollection<Review>> GetAllAsync()
        {
            try
            {
                var client = new GrpcClientServices.ReviewService.ReviewServiceClient(_channel);
                var reply = await client.GetAllReviewsAsync(new GetAllReviewsRequest());
                ICollection<Review> reviews = new List<Review>();
                foreach (var review in reply.Reviews)
                {
                    reviews.Add(new Review(
                        id: review.Id,
                        userId: review.CustomerId,
                        itemId: review.ItemId,
                        rating: review.Rating,
                        comment: review.Comment
                    ));
                }

                return reviews;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error in AddReviewAsync: {e.Message}");
                throw;
            }
        }
    }
}