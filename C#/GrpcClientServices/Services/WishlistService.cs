using Domain.Shopping.Models;
using Grpc.Net.Client;

namespace GrpcClientServices.Services;

public class WishlistService : GrpcClientServices.WishlistService.WishlistServiceClient
{
    private readonly GrpcChannel _channel;
    
    public WishlistService()
    {
        _channel = GrpcChannel.ForAddress("http://localhost:3030");
    }

    public async Task<Wishlist?> AddWishlistItemAsync(Wishlist wishlistItem)
    {
        try
        {
            GrpcWishlistItem wishlistItemToAdd = GenerateGrpcWishlistItem(wishlistItem);
            var client = new GrpcClientServices.WishlistService.WishlistServiceClient(_channel);
            var reply = await client.AddToWishlistAsync(new AddToWishlistRequest()
            {
                WishlistItem = wishlistItemToAdd
            });
            Wishlist? wishlistItemToReturn = GenerateWishlist(reply.WishlistItem);
            Console.WriteLine(reply);
            return wishlistItemToReturn;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        return null;
    }

    public async Task<ICollection<Wishlist?>> GetWishlistByUserIdAsync(int id)
    {
        try
        {
            var grpcClient = new GrpcClientServices.WishlistService.WishlistServiceClient(_channel);
            var reply = await grpcClient.GetWishlistByUserAsync(new GetWishlistByUserRequest()
            {
                UserId = id
            });
            ICollection<Wishlist?> wishlistItems = new List<Wishlist?>();
            foreach (var wishlist in reply.WishlistItems)
            {
                wishlistItems.Add(GenerateWishlist(wishlist));
            }

            return wishlistItems;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

        return new List<Wishlist?>();
    }

    private Wishlist? GenerateWishlist(GrpcWishlistItem wishlistItem)
    {
        Wishlist? generatedWishlistItem = null;
        /*generatedWishlistItem = new Wishlist(wishlistItem.ItemId, wishlistItem.UserId)
        {
            Id = wishlistItem.Id,
        }; */
        return generatedWishlistItem;
    }

    private GrpcWishlistItem GenerateGrpcWishlistItem(Wishlist wishlistItem)
    {
        GrpcWishlistItem generatedGrpcWishlistItem = new GrpcWishlistItem()
        {
            Id = wishlistItem.Id,
            ItemId = wishlistItem.ItemId?.Id ?? 0,
            UserId = wishlistItem.UserId?.Id ?? 0,
        };
        return generatedGrpcWishlistItem;
    }

}