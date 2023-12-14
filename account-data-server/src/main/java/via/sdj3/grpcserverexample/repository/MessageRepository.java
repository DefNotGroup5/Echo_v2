package via.sdj3.grpcserverexample.repository;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.data.jpa.repository.Modifying;
import org.springframework.data.jpa.repository.Query;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;
import via.sdj3.grpcserverexample.entities.ItemEntity;
import via.sdj3.grpcserverexample.entities.MessageEntity;

import java.util.Optional;

@Repository
public interface MessageRepository extends JpaRepository<MessageEntity, Integer> {
    @Query("SELECT m FROM MessageEntity m WHERE m.id = :id")
    Optional<MessageEntity> getMessageById(int id);
}
