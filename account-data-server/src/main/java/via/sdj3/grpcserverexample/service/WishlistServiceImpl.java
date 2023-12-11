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
  private ItemRepository itemRepository;
  private UserRepository userRepository;

  public WishlistServiceImpl(WishlistRepository wishlistRepository, ItemRepository itemRepository, UserRepository userRepository)
  {
    this.wishlistRepository = wishlistRepository;
    this.itemRepository = itemRepository;
    this.userRepository = userRepository;
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
    } catch (Exception e) {
      Status status = Status.INTERNAL.withDescription("Error adding item to wishlist");
      responseObserver.onError(new StatusRuntimeException(status));
    }
  }

  @Override
  public void getWishlistByUser(GetWishlistByUserRequest request, StreamObserver<GetWishlistByUserResponse> responseStreamObserver) {
    try {
      int userId = request.getUserId();
      List<WishlistEntity> wishlistItems = wishlistRepository.getByUser(userRepository.getReferenceById(userId));
      GetWishlistByUserResponse.Builder responseBuilder = GetWishlistByUserResponse.newBuilder();

      for (WishlistEntity wishlistItem : wishlistItems) {
        GrpcWishlistItem grpcWishlistItem = generateGrpcWishlistItem(wishlistItem);
        responseBuilder.addWishlistItems(grpcWishlistItem);
      }
      System.out.println("Wishlist for user: " + userId + "is gathered.");
      responseStreamObserver.onNext(responseBuilder.build());
      responseStreamObserver.onCompleted();
    } catch (Exception e) {
      Status status = Status.INTERNAL.withDescription("Error when getting wishlist by user");
      responseStreamObserver.onError(new StatusRuntimeException(status));
    }
  }


  private WishlistEntity generateWishlistEntity(GrpcWishlistItem wishlist){
    WishlistEntity wishlistEntity = new WishlistEntity();
    wishlistEntity.setId(wishlist.getId());
    ItemEntity itemEntity = itemRepository.findById(wishlist.getItemId()).orElse(null);
    wishlistEntity.setItem(itemEntity);
    UserEntity userEntity = userRepository.findById(wishlist.getUserId()).orElse(null);
    wishlistEntity.setUser(userEntity);
    return wishlistEntity;
  }

  private GrpcWishlistItem generateGrpcWishlistItem(WishlistEntity wishlistEntity) {
    ItemEntity itemEntity = wishlistEntity.getItem();
    UserEntity userEntity = wishlistEntity.getUser();
    return GrpcWishlistItem.newBuilder()
        .setId(wishlistEntity.getId())
        .setItemId(itemEntity != null ? itemEntity.getId() : 0) // Set item ID or handle null case
        .setUserId(userEntity != null ? userEntity.getId() : 0)
        .build();
  }
}
