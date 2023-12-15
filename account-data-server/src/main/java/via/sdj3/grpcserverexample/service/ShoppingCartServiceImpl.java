package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.ShoppingCartEntity;
import via.sdj3.grpcserverexample.repository.ShoppingCartRepository;
import via.sdj3.protobuf.cart.*;
import via.sdj3.protobuf.item.AddItemResponse;
import via.sdj3.protobuf.item.GetAllItemsResponse;
import via.sdj3.protobuf.item.GrpcItem;

import java.lang.ref.ReferenceQueue;
import java.util.List;

@GrpcService
public class ShoppingCartServiceImpl extends ShoppingCartServiceGrpc.ShoppingCartServiceImplBase {
    private ShoppingCartRepository shoppingCartRepository;

    public ShoppingCartServiceImpl(ShoppingCartRepository shoppingCartRepository)
    {
        this.shoppingCartRepository = shoppingCartRepository;
    }

    @Override
    public void addToShoppingCart(AddToShoppingCartRequest request, StreamObserver<AddToShoppingCartResponse> responseObserver) {
        try {
            ShoppingCartEntity cartEntity = new ShoppingCartEntity();
            cartEntity.setCustomer_id(request.getCustomerId());
            cartEntity.setQuantity(request.getQuantity());
            cartEntity.setItem_id(request.getItemId());
            ShoppingCartEntity returnEntity = shoppingCartRepository.save(cartEntity);
            GrpcCartItem cartItem = GrpcCartItem.newBuilder()
                    .setItemId(returnEntity.getItem_id())
                    .setQuantity(returnEntity.getQuantity())
                    .setCustomerId(returnEntity.getCustomer_id())
                    .setId(returnEntity.getId())
                    .build();
            AddToShoppingCartResponse response = AddToShoppingCartResponse.newBuilder().setCartItem(cartItem).build(); //makes a response to say the item was added
            System.out.println("The item was added to shopping cart.");
            responseObserver.onNext(response); //sends the response to the client
            responseObserver.onCompleted(); //operation complete
        }
        catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error adding item to shopping cart"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }

    @Override
    public void clearCart(ClearCartRequest request, StreamObserver<ClearCartResponse> responseObserver) {
        try
        {
            shoppingCartRepository.deleteByCustomerId(request.getCustomerId());
            responseObserver.onNext(ClearCartResponse.newBuilder().setResult("Cart cleared").build());
            System.out.println("Shopping Cart Cleared");
            responseObserver.onCompleted();
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error clearing cart"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }

    @Override
    public void getAllCartItems(GetAllCartItemsRequest request, StreamObserver<GetAllCartItemsResponse> responseObserver) {
        try
        {
            List<ShoppingCartEntity> entities = shoppingCartRepository.findAll();
            GetAllCartItemsResponse.Builder response = GetAllCartItemsResponse.newBuilder();
            for (ShoppingCartEntity entity : entities) {
                GrpcCartItem cartItem = GrpcCartItem.newBuilder()
                        .setId(entity.getId())
                        .setCustomerId(entity.getCustomer_id())
                        .setQuantity(entity.getQuantity())
                        .setItemId(entity.getItem_id()).build();
                response.addItems(cartItem);
            }
            System.out.println("The shopping cart items were gathered.");
            responseObserver.onNext(response.build()); //sends the response to the client
            responseObserver.onCompleted(); //operation complete
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error shopping cart adding item"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }
}
