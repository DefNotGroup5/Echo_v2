package via.sdj3.grpcserverexample.service;

import com.google.protobuf.Empty;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.UserEntity;
import via.sdj3.grpcserverexample.repository.UserRepository;
import via.sdj3.protobuf.users.*;
import via.sdj3.protobuf.users.AdminServiceGrpc;

import java.util.List;

@GrpcService
public class AdminServiceImpl extends AdminServiceGrpc.AdminServiceImplBase {
    private UserRepository userRepository;

    public AdminServiceImpl(UserRepository userRepository) {
        this.userRepository = userRepository;
    }

    @Override
    public void listSellers(Empty request, StreamObserver<ListUsersResponse> responseObserver) {
        try {
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
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error listing sellers");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    @Override
    public void authorizeSeller(AuthorizeSellerRequest request, StreamObserver<AuthorizeSellerResponse> responseObserver) {
        try {
            userRepository.setSellerAuthorization(request.getId(), request.getIsAuthorized());
            AuthorizeSellerResponse response = AuthorizeSellerResponse.newBuilder()
                    .setResult("Seller authorization updated.")
                    .build();
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error authorizing seller");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }
}
