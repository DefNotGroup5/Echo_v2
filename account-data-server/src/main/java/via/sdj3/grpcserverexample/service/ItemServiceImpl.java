package via.sdj3.grpcserverexample.service;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.ItemRepository;
import via.sdj3.protobuf.item.*;
import via.sdj3.protobuf.users.GrpcUser;

import java.util.List;
import java.util.Optional;

@GrpcService
public class ItemServiceImpl extends ItemServiceGrpc.ItemServiceImplBase {

    private ItemRepository itemRepository;


    public ItemServiceImpl(ItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    @Override
    public void addItem(AddItemRequest request, StreamObserver<AddItemResponse> responseObserver) {
        try
        {
            ItemEntity itemToAdd = generateItemEntity(request.getItem()); //takes the grpc item object and transforms it into a ItemEntity object
            ItemEntity itemEntity = itemRepository.save(itemToAdd);
            AddItemResponse response = AddItemResponse.newBuilder().setItem(generateGrpcItem(itemEntity)).build(); //makes a response to say the item was added
            System.out.println("The item was added.");
            responseObserver.onNext(response); //sends the response to the client
            responseObserver.onCompleted(); //operation complete
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error adding item"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }

    @Override public void getItemById(GetItemByIdRequest request, StreamObserver<GetItemByIdResponse> responseStreamObserver)
    {
        try
        {
            Optional<ItemEntity> existingItem = itemRepository.getItemById(request.getId()); //gets an item from the repository based on the id
            if(existingItem.isPresent()) {
                ItemEntity item = existingItem.get();
                GetItemByIdResponse response = GetItemByIdResponse.newBuilder().setItem(generateGrpcItem(item)).build(); //converts the item entity retrieved into a grpc self generated response message
                responseStreamObserver.onNext(response); //sends the response back to the client
                responseStreamObserver.onCompleted(); //process complete
            } else {
                // Handle the case when the item is not found
                Status status = Status.NOT_FOUND.withDescription("Item not found for the given ID");
                responseStreamObserver.onError(new StatusRuntimeException(status));
            }
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error when getting item by ID"); // if there is an error
            responseStreamObserver.onError(new StatusRuntimeException(status)); //sends the status to the client
        }
    }

    @Override
    public void getAllItems(GetAllItemsRequest request, StreamObserver<GetAllItemsResponse> responseObserver) {
        try
        {
            List<ItemEntity> items = itemRepository.findAll();
            GetAllItemsResponse.Builder response = GetAllItemsResponse.newBuilder();
            for (ItemEntity item : items) {
                GrpcItem grpcItem = generateGrpcItem(item);
                response.addItems(grpcItem);
            }
            System.out.println("The items were gathered.");
            responseObserver.onNext(response.build()); //sends the response to the client
            responseObserver.onCompleted(); //operation complete
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error adding item"); //message in case on error
            responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client
        }
    }

    private ItemEntity generateItemEntity(GrpcItem item)
{
    ItemEntity itemEntity = new ItemEntity();
    itemEntity.setId(item.getItemId());
    itemEntity.setSellerId(item.getSellerId());
    itemEntity.setName(item.getName());
    itemEntity.setImage_url(item.getImageUrl());
    itemEntity.setDescription(item.getDescription());
    itemEntity.setPrice(item.getPrice());
    itemEntity.setQuantity(item.getQuantity());
    return itemEntity;
}

    private GrpcItem generateGrpcItem(ItemEntity itemEntity)
    {
        return GrpcItem.newBuilder()
            .setSellerId(itemEntity.getSellerId())
            .setItemId(itemEntity.getId())
            .setName(itemEntity.getName())
            .setImageUrl(itemEntity.getImageUrl())
            .setDescription(itemEntity.getDescription())
            .setPrice(itemEntity.getPrice())
            .setQuantity(itemEntity.getQuantity())
            .build();
    }

}