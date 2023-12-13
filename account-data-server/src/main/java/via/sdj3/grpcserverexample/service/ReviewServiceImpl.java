package via.sdj3.grpcserverexample.service;

import net.devh.boot.grpc.server.service.GrpcService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sdj3.grpcserverexample.entities.ReviewEntity;
import via.sdj3.grpcserverexample.repository.ReviewRepository;
import via.sdj3.protobuf.review.ReviewServiceGrpc;

@GrpcService
public class ReviewServiceImpl extends ReviewServiceGrpc.ReviewServiceImplBase {

   /* private final ReviewRepository reviewRepository;

    @Autowired
    public ReviewServiceImpl(ReviewRepository reviewRepository) {
        this.reviewRepository = reviewRepository;
    }

//    public ReviewEntity addReview(ReviewEntity review) throws IllegalArgumentException {
//        validateReview(review);
//
//        int userId = review.getCustomer().getId();
//        int itemId = review.getItem().getId();
//
//        // Corrected the method call here
//        if(reviewRepository.findByUserIdAndItemId(userId, itemId).isPresent()) {
//            throw new IllegalArgumentException("User has already reviewed this item.");
//        }
//
//        return reviewRepository.save(review);
//    }

    private void validateReview(ReviewEntity review) {
        if (review.getRating() < 1 || review.getRating() > 5) {
            throw new IllegalArgumentException("Rating must be between 1 and 5.");
        }
    }*/
}