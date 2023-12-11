using Grpc.Net.Client;
using GrpcClientServices;
using System.Threading.Tasks;
using Domain.Account.Models;
using Domain.Shopping.Models; // Import your domain models namespace

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
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}