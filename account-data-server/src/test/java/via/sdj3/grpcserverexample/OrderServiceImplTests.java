package via.sdj3.grpcserverexample;

import io.grpc.stub.StreamObserver;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.boot.test.context.SpringBootTest;
import via.sdj3.grpcserverexample.entities.OrderEntity;
import via.sdj3.grpcserverexample.repository.OrderRepository;
import via.sdj3.grpcserverexample.service.OrderServiceImpl;
import via.sdj3.protobuf.order.*;
import jakarta.transaction.Transactional;
import com.google.protobuf.Timestamp;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@SpringBootTest
public class OrderServiceImplTests {

    @Mock
    private OrderRepository orderRepository;

    private OrderServiceImpl orderService;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
        orderService = new OrderServiceImpl(orderRepository);
    }

    @Test
    void contextLoads() {
        // This test ensures that the Spring context loads properly.
    }

    @Test
    @Transactional
    void testCreateOrderWithValidData() {
        CreateOrderRequest request = CreateOrderRequest.newBuilder()
                .setCustomerId(1)
                .setOrderDate(Timestamp.newBuilder().setSeconds(1234567890))
                .setTotalPrice(100.0)
                .setStatus("Pending")
                .addItemId(1)
                .addItemId(2)
                .build();

        StreamObserver<CreateOrderResponse> responseObserver = mock(StreamObserver.class);

        when(orderRepository.save(any(OrderEntity.class))).thenReturn(new OrderEntity());

        orderService.createOrder(request, responseObserver);

        verify(orderRepository, times(2)).save(any(OrderEntity.class));
        verify(responseObserver, times(1)).onNext(any(CreateOrderResponse.class));
        verify(responseObserver, times(1)).onCompleted();
    }
}