package via.sdj3.grpcserverexample.service;

import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import org.hibernate.sql.Update;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.protobuf.*;

import java.util.Optional;

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
            UserEntity userToBeAdded = generateUserEntity(request.getUser());
            userRepository.save(userToBeAdded);
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
            UserEntity existingUser = userRepository.getReferenceById(request.getId());
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
    
     @Override
        public void listSellers(Empty request, StreamObserver<ListUsersResponse> responseObserver) {
            List<UserEntity> sellers = userRepository.findByIsSeller(true);
            ListUsersResponse.Builder responseBuilder = ListUsersResponse.newBuilder();
            for (UserEntity seller : sellers) {
                GrpcUser grpcUser = GrpcUser.newBuilder()
                    .setId(seller.getId())
                    .setEmail(seller.getEmail())
                    .setIsAuthorizedSeller(seller.isAuthorizedSeller())
                    .build();
                responseBuilder.addUsers(grpcUser);
            }
            responseObserver.onNext(responseBuilder.build());
            responseObserver.onCompleted();
        }
     @Override
         public void authorizeSeller(AuthorizeSellerRequest request, StreamObserver<AuthorizeSellerResponse> responseObserver) {
             userRepository.setSellerAuthorization(request.getId(), request.getIsAuthorized());
             AuthorizeSellerResponse response = AuthorizeSellerResponse.newBuilder()
                 .setResult("Seller authorization updated.")
                 .build();
             responseObserver.onNext(response);
             responseObserver.onCompleted();
         }

    private UserEntity generateUserEntity(GrpcUser user)
    {
        UserEntity userEntity = new UserEntity();
        userEntity.setFirstName(user.getFirstName());
        userEntity.setLastName(user.getLastName());
        userEntity.setAddress(user.getAddress());
        userEntity.setEmail(user.getEmail());
        userEntity.setSeller(user.getIsSeller());
        userEntity.setCity(user.getCity());
        userEntity.setCountry(user.getCountry());
        userEntity.setPostalCode(user.getPostalCode());
        userEntity.setPassword(user.getPassword());
        return userEntity;
    }

    private GrpcUser generateGrpcUser(UserEntity userEntity)
    {
        GrpcUser user = GrpcUser.newBuilder().setId(userEntity.getId()).setFirstName(userEntity.getFirstName()).setLastName(userEntity.getLastName())
                .setAddress(userEntity.getAddress()).setEmail(userEntity.getEmail()).setIsSeller(userEntity.isSeller())
                .setCity(userEntity.getCity()).setCountry(userEntity.getCountry())
                .setPostalCode(userEntity.getPostalCode()).setPassword(userEntity.getPassword()).build();
        return user;
    }
    
    
}
