package via.sdj3.grpcserverexample.service;

import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.OrderEntity;
import via.sdj3.grpcserverexample.entities.OrderStatus;
import via.sdj3.grpcserverexample.entities.OrderItemEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.OrderRepository;
import via.sdj3.protobuf.order.OrderServiceGrpc;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

@GrpcService
public class OrderServiceImpl extends OrderServiceGrpc.OrderServiceImplBase {

    private final OrderRepository orderRepository;

    @Autowired
    public OrderServiceImpl(OrderRepository orderRepository) {
        this.orderRepository = orderRepository;
    }

    @Transactional
    public OrderEntity createOrder(UserEntity customer, Map<ItemEntity, Integer> items) {
        OrderEntity newOrder = new OrderEntity();
        newOrder.setCustomer(customer);
        newOrder.setOrderDate(new Date());
        newOrder.setStatus(OrderStatus.PENDING);

        double totalCost = 0;
        for (Map.Entry<ItemEntity, Integer> entry : items.entrySet()) {
            ItemEntity item = entry.getKey();
            int quantity = entry.getValue();
            OrderItemEntity orderItem = new OrderItemEntity(newOrder, item, quantity);
            newOrder.getItems().add(orderItem);
            totalCost += item.getPrice() * quantity;
        }

        newOrder.setTotalCost(totalCost);

        return orderRepository.save(newOrder);
    }
}
