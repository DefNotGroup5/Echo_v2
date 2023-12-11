using Grpc.Net.Client;
using GrpcClientServices;
using System.Threading.Tasks;
using Domain.Account.Models;
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

        public async Task<Review> AddReviewAsync(Review review)
        {
            try
            {
                GrpcReview grpcReview = new GrpcReview
                {
                    UserId = review.UserId,
                    ItemId = review.ItemId,
                    Rating = review.Rating,
                    Comment = review.Comment
                };

                var client = new GrpcClientServices.ReviewService.ReviewServiceClient(_channel);
                var reply = await client.AddReviewAsync(new AddReviewRequest { Review = grpcReview });

                return new Review(
                    id: reply.Review.Id,
                    userId: reply.Review.UserId,
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

        public async Task<bool> CheckReviewExistsAsync(int userId, int itemId)
        {
            var request = new CheckReviewExistsRequest { UserId = userId, ItemId = itemId };
            var client = new GrpcClientServices.ReviewService.ReviewServiceClient(_channel);
            var reply = await client.CheckReviewExistsAsync(request);

            return reply.Exists;
        }

        public async Task<List<Review>> GetReviewsByItemAsync(int itemId)
        {
            var request = new GetReviewsByItemRequest { ItemId = itemId };
            var client = new GrpcClientServices.ReviewService.ReviewServiceClient(_channel);
            var reply = await client.GetReviewsByItemAsync(request);

            var reviews = new List<Review>();
            foreach (var grpcReview in reply.Reviews)
            {
                reviews.Add(new Review(
                    id: grpcReview.Id,
                    userId: grpcReview.UserId,
                    itemId: grpcReview.ItemId,
                    rating: grpcReview.Rating,
                    comment: grpcReview.Comment
                ));
            }

            return reviews;
        }

        public async Task<List<Review>> GetReviewsByUserAsync(int userId)
        {
            var request = new GetReviewsByUserRequest { UserId = userId };
            var client = new GrpcClientServices.ReviewService.ReviewServiceClient(_channel);
            var reply = await client.GetReviewsByUserAsync(request);

            var reviews = new List<Review>();
            foreach (var grpcReview in reply.Reviews)
            {
                reviews.Add(new Review(
                    id: grpcReview.Id,
                    userId: grpcReview.UserId,
                    itemId: grpcReview.ItemId,
                    rating: grpcReview.Rating,
                    comment: grpcReview.Comment
                ));
            }

            return reviews;
        }
    }
}