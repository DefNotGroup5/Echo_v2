package via.sdj3.grpcserverexample.service;


import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.ItemEntityId;
import via.sdj3.grpcserverexample.repository.ItemRepository;
import via.sdj3.protobuf.*;

import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

@GrpcService
public class ItemServiceImpl extends ItemServiceGrpc.ItemServiceImplBase {

    private ItemRepository itemRepository;


    public ItemServiceImpl (ItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    public void add(AddItemRequest request, StreamObserver<AddItemResponse> responseObserver)
    {
        try
        {
        GrpcItem item = request.getGrpcItem(); //information from the request is put on an item object
        ItemEntity itemToAdd = generateItemEntity(request.getGrpcItem()); //takes the grpc item object and transforms it into a ItemEntity object
        itemRepository.save(itemToAdd); // saves the data to the database or repository
        AddItemResponse response = AddItemResponse.newBuilder().setResult("THe item was added.").setItemId(itemToAdd.getId().getItemId()).build(); //makes a response to say the item was added
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

    public void getById(GetItemByIdRequest request, StreamObserver<GetItemByIdResponse> responseStreamObserver)
    {
        try
        {
            ItemEntityId itemEntityId = new ItemEntityId(request.getItemId(), request.getSellerId());
            Optional<ItemEntity> itemOptional  = itemRepository.getByIId(itemEntityId); //gets an item from the repository based on the id
            if(itemOptional.isPresent()) {
                ItemEntity item = itemOptional.get();
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

    public void getAllItems(GetAllItemsByIdRequest request, StreamObserver<GetAllItemsByIdResponse> responseStreamObserver) {
        try
        {
            ItemEntityId itemEntityId = new ItemEntityId(request.getItemIid(), request.getSellerId());
            List<ItemEntity> itemList = itemRepository.getAllItemsById(itemEntityId);

            // Convert the list of ItemEntity to a list of GrpcItem
            List<GrpcItem> grpcItemList = itemList.stream()
                .map(this::generateGrpcItem)
                .collect(Collectors.toList());

            // Build the response with the list of GrpcItem
            GetAllItemsByIdResponse response = GetAllItemsByIdResponse.newBuilder().addAllItems(grpcItemList).build();

            responseStreamObserver.onNext(response);
            responseStreamObserver.onCompleted();
        }catch (Exception e) {

        }
    }

//    private ItemEntity generateItemEntity(GrpcItem item)
//    {
//        ItemEntity itemEntity = new ItemEntity();
//        itemEntity.setSellerId(item.getId());
//        itemEntity.setName(item.getName());
//        itemEntity.setImage_url(item.getImageUrl());
//        itemEntity.setDescription(item.getDescription());
//        itemEntity.setPrice(item.getPrice());
//        itemEntity.setQuantity(item.getQuantity());
//        itemEntity.setStock_available(item.getStock());
//        return itemEntity;
//    }
private ItemEntity generateItemEntity(GrpcItem item)
{
    ItemEntity itemEntity = new ItemEntity();
    ItemEntityId itemId = new ItemEntityId(item.getSellerId(), item.getItemId()); //Change constructor to accept only sellerId
    itemEntity.setId(itemId);
    itemEntity.setName(item.getName());
    itemEntity.setImage_url(item.getImageUrl());
    itemEntity.setDescription(item.getDescription());
    itemEntity.setPrice(item.getPrice());
    itemEntity.setQuantity(item.getQuantity());
    itemEntity.setStock_available(item.getStock());
    return itemEntity;
}

    private GrpcItem generateGrpcItem(ItemEntity itemEntity)
    {
        GrpcItem item = GrpcItem.newBuilder()
            .setSellerId(itemEntity.getId().getSellerId())
            .setItemId(itemEntity.getId().getItemId())
            .setName(itemEntity.getName())
            .setImageUrl(itemEntity.getImageUrl())
            .setDescription(itemEntity.getDescription())
            .setPrice(itemEntity.getPrice())
            .setQuantity(itemEntity.getQuantity())
            .setStock(itemEntity.getStock())
            .build();
        return item;
    }

}