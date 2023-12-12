package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.ReviewEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;

import java.util.List;
import java.util.Optional;

@Repository
public interface ReviewRepository extends JpaRepository<ReviewEntity, Integer> {
    List<ReviewEntity> findByItemId(int itemId);
    //List<ReviewEntity> findByUserId(int userId);
   // Optional<ReviewEntity> findByUserIdAndItemId(int userId, int itemId);

}
