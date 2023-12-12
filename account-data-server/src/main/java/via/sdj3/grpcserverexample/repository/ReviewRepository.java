package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.ReviewEntity;
import via.sdj3.grpcserverexample.entities.UserEntity;

import java.util.List;

@Repository
public interface ReviewRepository extends JpaRepository<ReviewEntity, Long> {
    List<ReviewEntity> findByItem(ItemEntity item);

   // List<ReviewEntity> findByUser(UserEntity user);

}
