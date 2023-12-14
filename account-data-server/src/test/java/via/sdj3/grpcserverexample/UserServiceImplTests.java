package via.sdj3.grpcserverexample;

import io.grpc.stub.StreamObserver;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.springframework.boot.test.context.SpringBootTest;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.grpcserverexample.service.UserServiceImpl;
import via.sdj3.protobuf.users.AddRequest;
import via.sdj3.protobuf.users.AddResponse;
import via.sdj3.protobuf.users.GrpcUser;

import static org.mockito.ArgumentMatchers.any;
import static org.mockito.Mockito.*;

@SpringBootTest
class UserServiceImplTests {

    @Mock
    private UserRepository userRepository;

    private UserServiceImpl userService;

    @BeforeEach
    void setUp() {
        MockitoAnnotations.openMocks(this);
        userService = new UserServiceImpl(userRepository);
    }

    @Test
    void contextLoads() {
        // This test ensures that the Spring context loads properly.
    }

    @Test
    void testAddUserWithValidData() {
        GrpcUser testUser = GrpcUser.newBuilder()
                .setFirstName("John")
                .setLastName("Doe")
                .setEmail("john.doe@example.com")
                .setPassword("password123")
                .setAddress("123 Main St")
                .setCity("Anytown")
                .setCountry("USA")
                .setPostalCode(12345)
                .setIsSeller(false)
                .build();

        AddRequest request = AddRequest.newBuilder().setUser(testUser).build();
        StreamObserver<AddResponse> responseObserver = mock(StreamObserver.class);

        // Assuming that the save() method returns the saved entity
        when(userRepository.save(any(UserEntity.class))).thenReturn(new UserEntity());

        userService.add(request, responseObserver);

        verify(userRepository, times(1)).save(any(UserEntity.class));
        verify(responseObserver, times(1)).onNext(any(AddResponse.class));
        verify(responseObserver, times(1)).onCompleted();
    }
}