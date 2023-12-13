package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.OrderEntity;
import via.sdj3.grpcserverexample.repository.OrderRepository;
import via.sdj3.protobuf.order.*;

@GrpcService
public class OrderServiceImpl extends OrderServiceGrpc.OrderServiceImplBase {

    private OrderRepository orderRepository;

    public OrderServiceImpl(OrderRepository orderRepository) {
        this.orderRepository = orderRepository;
    }

    @Override
    public void createOrder(CreateOrderRequest request, StreamObserver<CreateOrderResponse> responseObserver) {
        try {
            OrderEntity orderEntity = new OrderEntity();
            orderEntity.setCustomer_id(request.getCustomerId());
            orderEntity.setOrderDate(request.getOrderDate());
            orderEntity.setTotalCost(request.getTotalCost());
            orderEntity.setStatus(request.getStatus());
            orderEntity.setItem_id(request.getItemId()); // Assuming you want to store item_id in the order

            OrderEntity returnEntity = orderRepository.save(orderEntity);

            GrpcOrderItem orderItem = GrpcOrderItem.newBuilder()
                    .setId(returnEntity.getId())
                    .setCustomerId(returnEntity.getCustomer_id())
                    .setOrderDate(returnEntity.getOrderDate())
                    .setTotalCost(returnEntity.getTotalCost())
                    .setStatus(returnEntity.getStatus())
                    .setItemId(returnEntity.getItem_id())
                    .build();

            CreateOrderResponse response = CreateOrderResponse.newBuilder().setOrderItem(orderItem).build();
            System.out.println("Order created successfully.");
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error creating order");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    @Override
    public void getAllOrders(GetAllOrdersRequest request, StreamObserver<GetAllOrdersResponse> responseObserver) {
        try {
            List<OrderEntity> entities = orderRepository.findAll();
            GetAllOrdersResponse.Builder response = GetAllOrdersResponse.newBuilder();

            for (OrderEntity entity : entities) {
                GrpcOrderItem orderItem = GrpcOrderItem.newBuilder()
                        .setId(entity.getId())
                        .setCustomerId(entity.getCustomer_id())
                        .setOrderDate(entity.getOrderDate())
                        .setTotalCost(entity.getTotalCost())
                        .setStatus(entity.getStatus())
                        .setItemId(entity.getItem_id())
                        .build();
                response.addOrders(orderItem);
            }

            System.out.println("All orders retrieved successfully.");
            responseObserver.onNext(response.build());
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error retrieving orders");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }
}
