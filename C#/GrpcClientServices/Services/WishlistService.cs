using Domain.Shopping.DTOs;
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

    public async Task<Wishlist?> AddWishlistItemAsync(WishlistCreationDto dto)
    {
        try
        {
            GrpcWishlistItem wishlistItemToAdd = GenerateGrpcWishlistItem(new Wishlist()
            {
                ItemId = dto.ItemId,
                UserId = dto.UserId
            });
            var client = new GrpcClientServices.WishlistService.WishlistServiceClient(_channel);
            var reply = await client.AddToWishlistAsync(new AddToWishlistRequest()
            {
                WishlistItem = wishlistItemToAdd
            });
            Wishlist? wishlistItemToReturn = GenerateWishlist(reply.WishlistItem);
            return wishlistItemToReturn;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
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

    public async Task RemoveWishlist(int id)
    {
        try
        {
            var grpcClient = new GrpcClientServices.WishlistService.WishlistServiceClient(_channel);
            await grpcClient.RemoveWishlistAsync(new RemoveWishlistRequest()
            {
                Id = id
            });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Wishlist? GenerateWishlist(GrpcWishlistItem wishlistItem)
    {
        return new Wishlist()
        {
            ItemId = wishlistItem.ItemId,
            Id = wishlistItem.Id,
            UserId = wishlistItem.UserId
        };
    }

    private GrpcWishlistItem GenerateGrpcWishlistItem(Wishlist wishlistItem)
    {
        return new GrpcWishlistItem()
        {
            ItemId = wishlistItem.ItemId,
            UserId = wishlistItem.UserId
        };
    }

}