package via.sdj3.grpcserverexample.service;

import io.grpc.Grpc;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.ItemRepository;
//import via.sdj3.protobuf.*;

import java.util.stream.Stream;

//@GrpcService
//public class ItemServiceImpl //extends ItemServiceGrpc.ItemServiceImplBase{

  //  private ItemRepository itemRepository;

  //  public ItemServiceImpl (ItemRepository itemRepository)
    //{
      //  this.itemRepository = itemRepository;
    //}


   // public void add(AddRequest request, StreamObserver<AddResponse> responseObserver)
  //  {
    //    try{
//
  //          GrpcItem item = request.getItem(); //information from the request is put on an item object
     //       ItemEntity itemToAdd = generateItemEntity(request.getItem()); //takes the grpc item object and transforms it into a ItemEntity object
    //        itemRepository.save(itemToAdd); // saves the data to the database or repository
      //      AddResponse response = AddResponse.newBuilder().setResult("THe item was added.").build(); //makes a response to say the item was added
        //    System.out.println("The item was added.");
          //  responseObserver.onNext(response); //sends the response to the client
     //       responseObserver.onCompleted(); //operation complete
       // }
      //  catch (Exception e)
        //{
          //  Status status = Status.INTERNAL.withDescription("Error adding item"); //message in case on error
     //       responseObserver.onError(new StatusRuntimeException(status)); //sends it to the client


      //  }
   // }

   // public void getById(GetByIdRequest request, StreamObserver<GetByIdResponse> responseStreamObserver){
  //      try{
    //        ItemEntity item = itemRepository.getReferenceById(request.getId()); //gets an item from the repository based on the id
    //        GetByIdResponse response = GetByIdResponse.newBuilder().setUser(generateGrpcItem(item)).build(); //converts the item entity retrieved into a grpc self generated response message
    //        responseStreamObserver.onNext(response); //sends the response back to the client
     //       responseStreamObserver.onCompleted(); //process complete
     //   }catch (Exception e)
       // {
     //       Status status = Status.INTERNAL.withDescription("Error when getting item by ID"); // if there is an error
       //     responseStreamObserver.onError(new StatusRuntimeException(status)); //sends the status to the client
      //  }
   // }


 //   private ItemEntity generateItemEntity(GrpcItem item)
 //   {
 //       ItemEntity itemEntity = new ItemEntity();
 //       itemEntity.setSellerId(item.getSellerId());
  //      itemEntity.setName(item.getName());
  //      itemEntity.setImage_url(item.getImageUrl());
  //      itemEntity.setDescription(item.getDescription());
  //      itemEntity.setPrice(item.getPrice());
  //      itemEntity.setQuantity(item.getQuantity());
  //      itemEntity.setStock_available(item.getStockAvailable());
  //      return itemEntity;
  //  }


  //  private GrpcUser generateGrpcItem(ItemEntity itemEntity)
    //{
    //    GrpcItem item = GrpcItem.newBuilder()
     //           .setId(itemEntity.getSellerId()).setName(itemEntity.getName()).setImage_url(itemEntity.getImageUrl())
     //           .setDescription(itemEntity.getDescription()).setPrice(itemEntity.getPrice())
     //           .setQuantity(itemEntity.getQuantity()).setStock_available(itemEntity.getStockAvailable())
      //          .build();
      //  return item;
   // }





//}
