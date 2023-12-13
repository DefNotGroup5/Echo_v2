package via.sdj3.grpcserverexample.service;

import com.google.protobuf.Empty;
import io.grpc.Status;
import io.grpc.StatusRuntimeException;
import io.grpc.stub.StreamObserver;
import net.devh.boot.grpc.server.service.GrpcService;
import via.sdj3.grpcserverexample.entities.ReviewEntity;
import via.sdj3.grpcserverexample.repository.ReviewRepository;
import via.sdj3.protobuf.review.*;

import java.util.List;

@GrpcService
public class ReviewServiceImpl extends ReviewServiceGrpc.ReviewServiceImplBase {

    private ReviewRepository reviewRepository;

    public ReviewServiceImpl(ReviewRepository reviewRepository) {
        this.reviewRepository = reviewRepository;
    }

    @Override
    public void createReview(CreateReviewRequest request, StreamObserver<CreateReviewResponse> responseObserver) {
        try {
            ReviewEntity reviewEntity = new ReviewEntity();
            reviewEntity.setCustomer_id(request.getCustomerId());
            reviewEntity.setItem_id(request.getItemId());
            reviewEntity.setRating(request.getRating());
            reviewEntity.setComment(request.getComment());

            ReviewEntity returnEntity = reviewRepository.save(reviewEntity);

            GrpcReview grpcReview = GrpcReview.newBuilder()
                    .setId(returnEntity.getId())
                    .setCustomerId(returnEntity.getCustomer_id())
                    .setItemId(returnEntity.getItem_id())
                    .setRating(returnEntity.getRating())
                    .setComment(returnEntity.getComment())
                    .build();

            CreateReviewResponse response = CreateReviewResponse.newBuilder().setReview(grpcReview).build();
            System.out.println("Review created successfully.");
            responseObserver.onNext(response);
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error creating review");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }

    @Override
    public void getAllReviews(GetAllReviewsRequest request, StreamObserver<GetAllReviewsResponse> responseObserver) {
        try {
            List<ReviewEntity> entities = reviewRepository.findAll();
            GetAllReviewsResponse.Builder response = GetAllReviewsResponse.newBuilder();

            for (ReviewEntity entity : entities) {
                GrpcReview grpcReview = GrpcReview.newBuilder()
                        .setId(entity.getId())
                        .setCustomerId(entity.getCustomer_id())
                        .setItemId(entity.getItem_id())
                        .setRating(entity.getRating())
                        .setComment(entity.getComment())
                        .build();
                response.addReviews(grpcReview);
            }

            System.out.println("All reviews retrieved successfully.");
            responseObserver.onNext(response.build());
            responseObserver.onCompleted();
        } catch (Exception e) {
            Status status = Status.INTERNAL.withDescription("Error retrieving reviews");
            responseObserver.onError(new StatusRuntimeException(status));
        }
    }
}
