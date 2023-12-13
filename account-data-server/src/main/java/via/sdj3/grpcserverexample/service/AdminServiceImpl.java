package via.sdj3.grpcserverexample.service;

import com.google.protobuf.Empty;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.protobuf.users.*;
import via.sdj3.protobuf.users.admin.*;



import java.util.List;
import java.util.Optional;

@GrpcService
public class AdminServiceImpl extends AdminServiceGrpc.AdminServiceImplBase {
    private UserRepository userRepository;

    public AdminServiceImpl(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public void listSellers(ListUsersRequest request, StreamObserver<ListUsersResponse> responseObserver) {
        try {
            List<UserEntity> sellers = userRepository.findByIsSeller();
            ListUsersResponse.Builder responseBuilder = ListUsersResponse.newBuilder();
            for (UserEntity seller : sellers) {
                GrpcUser grpcUser = UserServiceImpl.generateGrpcUser(seller);
                responseBuilder.addUsers(grpcUser);
            }
            responseObserver.onNext(responseBuilder.build());
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error listing sellers");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }



    @Override
    public void authorizeSeller(ChangeSellerAuthorizationRequest request, StreamObserver<ChangeSellerAuthorizationResponse> responseObserver) {
        try {
            Optional<UserEntity> oldUser = userRepository.getById(request.getId());
            if (oldUser.isEmpty()) {
                Status status = Status.INTERNAL.withDescription("Error authorizing seller");
                responseObserver.onError(new StatusRuntimeException(status));
            }
            if (oldUser.isPresent()) {
                oldUser.get().setAuthorizedSeller(request.getAuthorizationState());
                userRepository.save(oldUser.get());
                ChangeSellerAuthorizationResponse response = ChangeSellerAuthorizationResponse.newBuilder()
                        .setResult("Seller authorization updated.")
                        .build();
                responseObserver.onNext(response);
                responseObserver.onCompleted();
            }
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error authorizing seller: " + e.getMessage());
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

}
