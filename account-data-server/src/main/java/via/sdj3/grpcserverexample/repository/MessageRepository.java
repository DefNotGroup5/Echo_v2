package via.sdj3.grpcserverexample.repository;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;
import via.sdj3.grpcserverexample.entities.MessageEntity;

@Repository
public interface MessageRepository extends JpaRepository<MessageEntity, Integer> {
}
