package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.entities.WishlistEntity;
import via.sdj3.grpcserverexample.repository.ItemRepository;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.grpcserverexample.repository.WishlistRepository;
import via.sdj3.protobuf.wishlist.*;

import java.util.List;

@GrpcService
public class WishlistServiceImpl extends WishlistServiceGrpc.WishlistServiceImplBase
{
  private WishlistRepository wishlistRepository;

  public WishlistServiceImpl(WishlistRepository wishlistRepository)
  {
    this.wishlistRepository = wishlistRepository;
  }

  @Override
  public void addToWishlist(AddToWishlistRequest request, StreamObserver<AddToWishlistResponse> responseObserver) {
      try {
          WishlistEntity wishlistItemToAdd = generateWishlistEntity(request.getWishlistItem());
          WishlistEntity wishlistEntity = wishlistRepository.save(wishlistItemToAdd);
          AddToWishlistResponse response = AddToWishlistResponse.newBuilder()
              .setWishlistItem(generateGrpcWishlistItem(wishlistEntity)).build();
          System.out.println("The item was added to the wishlist.");
          responseObserver.onNext(response);
          responseObserver.onCompleted();
      }
      catch (Exception e) {
          Status status = Status.INTERNAL.withDescription("Error adding item to wishlist");
          responseObserver.onError(new StatusRuntimeException(status));
      }
  }

  @Override
  public void getWishlistByUser(GetWishlistByUserRequest request, StreamObserver<GetWishlistByUserResponse> responseStreamObserver) {
      try {
          List<WishlistEntity> entities = wishlistRepository.getWishlistByUserId(request.getUserId());
          GetWishlistByUserResponse.Builder responseBuilder = GetWishlistByUserResponse.newBuilder();
          for(WishlistEntity entity : entities) {
            responseBuilder.addWishlistItems(generateGrpcWishlistItem(entity));
          }
          responseStreamObserver.onNext(responseBuilder.build());
          responseStreamObserver.onCompleted();
      } catch (Exception e) {
          Status status = Status.INTERNAL.withDescription("Error when getting wishlist by user");
          responseStreamObserver.onError(new StatusRuntimeException(status));
      }
  }

    @Override
    public void removeWishlist(RemoveWishlistRequest request, StreamObserver<RemoveWishlistResponse> responseObserver) {
        try {
            wishlistRepository.deleteById(request.getId());
            RemoveWishlistResponse response = RemoveWishlistResponse.newBuilder().setResult("Wishlist removed successfully").build();
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        }
        catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error when getting wishlist by user");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    private WishlistEntity generateWishlistEntity(GrpcWishlistItem wishlist){
    WishlistEntity wishlistEntity = new WishlistEntity();
    wishlistEntity.setId(wishlist.getId());
    wishlistEntity.setUserId(wishlist.getUserId());
    wishlistEntity.setItemId(wishlist.getItemId());
    return wishlistEntity;
  }

  private GrpcWishlistItem generateGrpcWishlistItem(WishlistEntity wishlistEntity) {
    return GrpcWishlistItem.newBuilder()
        .setId(wishlistEntity.getId())
        .setItemId(wishlistEntity.getItemId())
        .setUserId(wishlistEntity.getUserId())
        .build();
  }
}
