package via.sdj3.grpcserverexample.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import via.sdj3.grpcserverexample.entities.ReviewEntity;
import via.sdj3.grpcserverexample.repository.ReviewRepository;

@Service
public class ReviewServiceImpl {

    private final ReviewRepository reviewRepository;

    @Autowired
    public ReviewServiceImpl(ReviewRepository reviewRepository) {
        this.reviewRepository = reviewRepository;
    }

    public ReviewEntity addReview(ReviewEntity review) throws IllegalArgumentException {
        validateReview(review);

        return reviewRepository.save(review);
    }

    private void validateReview(ReviewEntity review) {
        if (review.getRating() < 1 || review.getRating() > 5) {
            throw new IllegalArgumentException("Rating must be between 1 and 5.");
        }
    }
}