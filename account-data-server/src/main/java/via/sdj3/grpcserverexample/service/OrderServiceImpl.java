package via.sdj3.grpcserverexample.service;

import com.google.protobuf.Timestamp;
import com.google.protobuf.Empty;
import org.apache.commons.codec.digest.DigestUtils;
import org.hibernate.sql.Update;

import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Random;

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
            String orderId = generateOrderId(request.getCustomerId(), request.getOrderDate().getSeconds());
            for(int itemId : request.getItemIdList())
            {
                OrderEntity orderEntity = new OrderEntity();
                orderEntity.setCustomer_id(request.getCustomerId());
                orderEntity.setOrderDate(request.getOrderDate());
                orderEntity.setTotalPrice(request.getTotalPrice());
                orderEntity.setStatus(request.getStatus());
                orderEntity.setOrder_id(orderId);
                orderEntity.setItem_id(itemId);
                orderRepository.save(orderEntity);
            }
            GrpcOrderItem orderItem = GrpcOrderItem.newBuilder()
                    .setCustomerId(request.getCustomerId())
                    .setOrderDate(request.getOrderDate())
                    .setTotalPrice(request.getTotalPrice())
                    .setStatus(request.getStatus())
                    .addAllItemId(request.getItemIdList())
                    .setOrderId(orderId)
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
            Map<String, GrpcOrderItem.Builder> orderItemBuilders = new HashMap<>();
            for(OrderEntity entity : entities)
            {
                String orderId = entity.getOrder_id();
                orderItemBuilders.computeIfAbsent(orderId, key -> GrpcOrderItem.newBuilder()
                        .setCustomerId(entity.getCustomer_id())
                        .setOrderDate(entity.getOrderDate())
                        .setTotalPrice(entity.getTotalPrice())
                        .setStatus(entity.getStatus())
                        .setOrderId(orderId));
                orderItemBuilders.get(orderId).addItemId(entity.getItem_id());
            }

            orderItemBuilders.values().forEach(builder -> response.addOrders(builder.build()));
            System.out.println("All orders retrieved successfully.");
            responseObserver.onNext(response.build());
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error retrieving orders");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    private String generateOrderId(int customerId, long orderDateSeconds) {
        String uniquePart = customerId + "_" + orderDateSeconds + "_" + generateRandomNumber();
        String orderId = hashFunction(uniquePart);
        return orderId.substring(0, 6);
    }

    private int generateRandomNumber() {
       return new Random().nextInt(1000000);
    }

    private String hashFunction(String input) {
        return DigestUtils.md5Hex(input);
    }
}
