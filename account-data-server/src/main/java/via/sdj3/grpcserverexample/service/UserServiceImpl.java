package via.sdj3.grpcserverexample.service;

import com.google.protobuf.Empty;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.hibernate.sql.Update;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.protobuf.users.*;
import via.sdj3.protobuf.users.UsersServiceGrpc;

import java.util.List;
import java.util.Optional;

@GrpcService //Important
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
            System.out.println(request.getUser().getAddress());
            UserEntity userToBeAdded = generateUserEntity(request.getUser()); //Generate User Entity from GrpcUser from request from client
            userRepository.save(userToBeAdded); //Save to repository
            AddResponse response = AddResponse.newBuilder().setResult("User Added!").build(); //Create response (weird syntax, don't ask)
            System.out.println("User added"); //Logging
            responseObserver.onNext(response); //On next available spot, execute
            responseObserver.onCompleted(); //Complete
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error adding user"); //Create an error status
            responseObserver.onError(new StatusRuntimeException(status)); //Return error status to client
        }
    }

    @Override
    public void update(UpdateRequest request, StreamObserver<UpdateResponse> responseObserver) {
        try
        {

            GrpcUser user = request.getUser();
            UserEntity oldUser = userRepository.findById(user.getId())
                    .orElseThrow(() -> new StatusRuntimeException(Status.NOT_FOUND.withDescription("User not found")));
            UserEntity updatedUser = generateUserEntity(request.getUser());
            updatedUser.setId(request.getUser().getId());
            userRepository.save(updatedUser);
            userRepository.findAll().remove(oldUser);
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

    @Override
    public void getByEmail(GetByEmailRequest request, StreamObserver<GetByEmailResponse> responseObserver) {
        try
        {
            Optional<UserEntity> existingUser = userRepository.getByEmail(request.getEmail());
            GetByEmailResponse response = GetByEmailResponse.newBuilder().setUser(generateGrpcUser(existingUser.get())).build();
           responseObserver.onNext(response);
           responseObserver.onCompleted();
            System.out.println("Ya");
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error getting by email!");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    @Override
    public void getById(GetByIdRequest request, StreamObserver<GetByIdResponse> responseObserver) {
        try
        {
            UserEntity existingUser = null;
            Optional<UserEntity> user = userRepository.getById(request.getId());
            if(user.isPresent())
            {
                existingUser = user.get();
            }
            else
            {
                Status status = Status.INTERNAL.withDescription("User not found!");
                responseObserver.onError(new StatusRuntimeException(status));
            }
            assert existingUser != null;
            GetByIdResponse response = GetByIdResponse.newBuilder().setUser(generateGrpcUser(existingUser)).build();
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        }
        catch (Exception e)
        {
            Status status = Status.INTERNAL.withDescription("Error getting by ID!");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    public static UserEntity generateUserEntity(GrpcUser user) //Helping Method for generating UserEntity from GrpcUser
    {
        UserEntity userEntity = new UserEntity();
        userEntity.setFirstName(user.getFirstName());
        userEntity.setLastName(user.getLastName());
        userEntity.setAddress(user.getAddress());
        userEntity.setEmail(user.getEmail());
        userEntity.setIsSeller(user.getIsSeller());
        userEntity.setCity(user.getCity());
        userEntity.setCountry(user.getCountry());
        userEntity.setPostalCode(user.getPostalCode());
        userEntity.setPassword(user.getPassword());
        return userEntity;
    }

    public static GrpcUser generateGrpcUser(UserEntity userEntity) //The other way
    {
        return GrpcUser.newBuilder().setId(userEntity.getId()).setFirstName(userEntity.getFirstName()).setLastName(userEntity.getLastName())
                .setAddress(userEntity.getAddress()).setEmail(userEntity.getEmail()).setIsSeller(userEntity.isSeller())
                .setCity(userEntity.getCity()).setCountry(userEntity.getCountry())
                .setPostalCode(userEntity.getPostalCode()).setPassword(userEntity.getPassword()).build();
    }
    
    
}
