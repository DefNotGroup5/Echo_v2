package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.hibernate.sql.Update;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.protobuf.*;

@GrpcService
public class UserServiceImpl extends UsersServiceGrpc.UsersServiceImplBase {
    private UserRepository userRepository;

    public UserServiceImpl(UserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    @Override
    public void add(AddRequest request, StreamObserver<AddResponse> responseObserver) {
        try
        {
            UserEntity user = new UserEntity();
            user.setId(request.getId());
            user.setEmail(request.getEmail());
            user.setFirstName(request.getFirstName());
            user.setLastName(request.getLastName());
            user.setPassword(request.getPassword());
            user.setAddress(request.getAddress());
            user.setCity(request.getCity());
            user.setPostalCode(request.getPostalCode());
            user.setCountry(request.getCountry());
            user.setSeller(request.getIsSeller());
            user.setLoggedIn(request.getIsLoggedIn());

            userRepository.save(user);
            AddResponse response = AddResponse.newBuilder().setResult("User Added!").build();
            System.out.println("User added");
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error adding user");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    @Override
    public void update(UpdateRequest request, StreamObserver<UpdateResponse> responseObserver) {
        try
        {
            UserEntity old = userRepository.getReferenceById(request.getId());
            UserEntity user = old;
            user.setId(request.getId());
            user.setEmail(request.getEmail());
            user.setFirstName(request.getFirstName());
            user.setLastName(request.getLastName());
            user.setPassword(request.getPassword());
            user.setAddress(request.getAddress());
            user.setCity(request.getCity());
            user.setPostalCode(request.getPostalCode());
            user.setCountry(request.getCountry());
            user.setSeller(request.getIsSeller());
            user.setLoggedIn(request.getIsLoggedIn());

            userRepository.save(user);
            userRepository.findAll().remove(old);

            UpdateResponse response = UpdateResponse.newBuilder().setResult("User updated!").build();
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error updating user");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }
}
